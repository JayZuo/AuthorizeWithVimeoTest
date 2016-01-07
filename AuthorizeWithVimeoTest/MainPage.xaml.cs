using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AuthorizeWithVimeoTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            TBResponseData.Text = await AuthorizeWithVimeo();
        }

        public async Task<string> AuthorizeWithVimeo()
        {
            var clientId = "b8e1bff5d5d1f2c90f61017b135960adb42f5fe2";

            var SpotifyUrl = "https://api.vimeo.com/oauth/authorize?client_id=" + Uri.EscapeDataString(clientId) + "&response_type=code&redirect_uri=" + Uri.EscapeDataString("https://example/callback") + "&state=xyzbc";
            var StartUri = new Uri(SpotifyUrl);
            var EndUri = new Uri("https://example/callback");

            WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, StartUri, EndUri);
            if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
                var responseData = WebAuthenticationResult.ResponseData;

                //await GetSpotifyUserNameAsync(WebAuthenticationResult.ResponseData.ToString());
                return responseData;
            }
            else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                return $"HTTP Error returned by AuthenticateAsync() : {WebAuthenticationResult.ResponseErrorDetail.ToString()}";
            }
            else
            {
                return $"Error returned by AuthenticateAsync() : {WebAuthenticationResult.ResponseStatus.ToString()}";
            }
        }
    }
}