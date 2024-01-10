using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppBenavidesAnthonyCurdMovil
{
    public partial class MainPage : ContentPage
    {
        string apiUrl = "https://ajbenavidespapi.azurewebsites.net/api/Restaurantes";
        public MainPage()
        {
            InitializeComponent();
        }

        private void cmdRelleno(object sender, EventArgs e)
        { }
        private void cmdInsertarRestaurante(object sender, EventArgs e) 
        {
            try 
            {
                using(var webClient = new HttpClient())
                {
                    webClient.BaseAddress = new Uri(apiUrl);
                    webClient
                    .DefaultRequestHeaders
                    .Accept
                    .Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
                    .Parse("application/json"));

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(new Restaurante
                    {
                        Id = int.Parse(txtIdRestaurante.Text),
                        Nombre = txtNombreRestaurante.Text,
                        Estrellas = int.Parse(txtEstrellasRestaurante.Text),
                        Especialidad = txtEspecialidadRestaurante.Text,
                        Ciudad = txtCiudadRestaurante.Text,
                        Direccion = txtDireccionRestaurante.Text
                    });

                    var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    var resp = webClient.SendAsync(request);
                    resp.Wait();

                    json = resp.Result.Content.ReadAsStringAsync().Result;
                    var prod = JsonConvert.DeserializeObject<Restaurante>(json);

                    txtIdRestaurante.Text = prod.Id.ToString();
                }
            } catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void cmdActualizarRestaurante(object sender, EventArgs e)
        {
            try 
            {
                using (var client = new HttpClient())
                {
                    var url = $"{apiUrl}/{txtIdRestaurante.Text}";
                    client.BaseAddress = new Uri(url);
                    client
                        .DefaultRequestHeaders
                        .Accept
                        .Add(System.Net.Http.Headers.
                        MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var json = JsonConvert.SerializeObject(new Restaurante
                    {
                        Id = int.Parse(txtIdRestaurante.Text),
                        Nombre = txtNombreRestaurante.Text,
                        Estrellas = int.Parse(txtEstrellasRestaurante.Text),
                        Especialidad = txtEspecialidadRestaurante.Text,
                        Ciudad = txtCiudadRestaurante.Text,
                        Direccion = txtDireccionRestaurante.Text
                    });

                    var rqst = new HttpRequestMessage(HttpMethod.Put, url);
                    rqst.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    var resp = client.SendAsync(rqst);
                    resp.Wait();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void cmdBuscarRestaurante(object sender, EventArgs e)
        {
            try
            {
                using (var webClient = new HttpClient())
                {
                    var resp = webClient.GetStringAsync(apiUrl + "/" + txtIdRestaurante.Text);
                    resp.Wait();

                    var json = resp.Result;
                    var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<Restaurante>(json);

                    txtIdRestaurante.Text = prod.Id.ToString();
                    txtNombreRestaurante.Text = prod.Nombre;
                    txtEstrellasRestaurante.Text = prod.Estrellas.ToString();
                    txtEspecialidadRestaurante.Text = prod.Especialidad;
                    txtCiudadRestaurante.Text = prod.Ciudad;
                    txtDireccionRestaurante.Text = prod.Direccion;
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void cmdEliminarRestaurante(object sender, EventArgs args)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{apiUrl}/{txtIdRestaurante.Text}";
                    client.BaseAddress = new Uri(url);
                    client
                        .DefaultRequestHeaders
                        .Accept
                        .Add(System.Net.Http.Headers.
                        MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var resp = client.DeleteAsync(url);
                    resp.Wait();

                    txtIdRestaurante.Text = "";
                    txtNombreRestaurante.Text = "";
                    txtEstrellasRestaurante.Text = "";
                    txtEspecialidadRestaurante.Text = "";
                    txtCiudadRestaurante.Text = "";
                    txtDireccionRestaurante.Text = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
