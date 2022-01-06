using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Prowler.KeyPass.Presentation.Helper
{
    internal static class IconHelper
    {

        public static readonly List<string> IconResource = new List<string>
        {
           (@"/Images/Icons/key_light.png"),
           (@"/Images/Icons/web_light.png"),
           (@"/Images/Icons/coud-icon.png"),
           (@"/Images/Actions/user_dark.png"),
           (@"/Images/Icons/steam-icon.png"),
           (@"/Images/Icons/wow-icon.png"),
           (@"/Images/Icons/netflix-icon.png"),
           (@"/Images/Icons/ps-icon.png"),
           (@"/Images/Icons/windows-icon.png"),
           (@"/Images/Icons/xbox-icon.png"),
           (@"/Images/Icons/google-icon.png"),
           (@"/Images/Icons/mail-icon.png"),
           (@"/Images/Icons/rdp-icon.png"),
           (@"/Images/Icons/prowler-icon.png"),           
           (@"/Images/Icons/git01-icon.png"),
           (@"/Images/Icons/card-icon.png"),
           (@"/Images/Icons/1298711_logo_skype_chat_communication_message_icon.png"),
           (@"/Images/Icons/1298714_photoshop_ps_psd_adobe_graphic_icon.png"),
           (@"/Images/Icons/1298718_drive_google_data_google drive_storage_icon.png"),
           (@"/Images/Icons/1298762_shopify_icon.png"),
           (@"/Images/Icons/1298766_spotify_music_sound_icon.png"),
           (@"/Images/Icons/1298767_tinder_icon.png"),
           (@"/Images/Icons/1298772_viber_chat_logo_messenger_talk_icon.png"),
           (@"/Images/Icons/1298776_wordpress_icon.png"),           
           (@"/Images/Icons/1920522_dropbox_logo_network_social_icon.png"),
           (@"/Images/Icons/2613285_amazon_cloud computing_commerce_company_electronics_icon.png"),
           (@"/Images/Icons/2613310_chat_email_messenger_social media_web service_icon.png"),
           (@"/Images/Icons/291690_viddler_movie_social_social media_logo_icon.png"),
           (@"/Images/Icons/291692_social media_social_logo_pinterest_icon.png"),
           (@"/Images/Icons/291697_soundcloud_sound_music_social_logo_icon.png"),
           (@"/Images/Icons/291716_github_logo_social network_social_icon.png"),           
           (@"/Images/Icons/2993728_treehouse_social media_icon.png"),
           (@"/Images/Icons/2993732_wechat_social media_icon.png"),
           (@"/Images/Icons/3057666_media_network_rss_social_icon.png"),
           (@"/Images/Icons/3171663_link_share_icon.png"),
           (@"/Images/Icons/317717_operating system_windows_icon.png"),
           (@"/Images/Icons/317720_social media_tweet_twitter_social_icon.png"),           
           (@"/Images/Icons/317752_facebook_social media_social_icon.png"),
           (@"/Images/Icons/317757_apple_macintosh_steve jobs_icon.png"),
           (@"/Images/Icons/3838998_bitcoin_cryptocurrency_currency_money_finance_icon.png"),           
           (@"/Images/Icons/4102585_applications_media_myspace_social_icon.png"),
           (@"/Images/Icons/4102606_applications_media_social_whatsapp_icon.png"),
           (@"/Images/Icons/4102608_applications_media_social_tumblr_icon.png"),
           (@"/Images/Icons/4201998_overflow_question_stock_stockoverflow_logo_icon.png"),
           (@"/Images/Icons/4202006_chrome_logo_social_social media_icon.png"),
           (@"/Images/Icons/4202013_browser_firefox_logo_social_social media_icon.png"),
           (@"/Images/Icons/4202014_finder_apple_logo_social_social media_icon.png"),
           (@"/Images/Icons/4202027_baidu_logo_social_social media_icon.png"),
           (@"/Images/Icons/4202032_amd_logo_social_social media_icon.png"),
           (@"/Images/Icons/4202044_media_player_video_windows_icon.png"),
           (@"/Images/Icons/4202051_utorrent_logo_social_social media_torrent_icon.png"),
           (@"/Images/Icons/4202081_paypal_logo_payment_social_social media_icon.png"),
           (@"/Images/Icons/4202089_nvidia_game_graphics_icon.png"),
           (@"/Images/Icons/4202093_msn_logo_social_social media_icon.png"),
           (@"/Images/Icons/4202103_office_logo_microsoft_ms_social_icon.png"),
           (@"/Images/Icons/4202108_edge_browser_e_logo_icon.png"),           
           (@"/Images/Icons/4550863_classmates_education_learn_learning_teaching_icon.png"),
           (@"/Images/Icons/4550868_calling_chat_chatting_viber_video calling_icon.png"),
           (@"/Images/Icons/4975311_business_chart_data_graph_report_icon.png"),
           (@"/Images/Icons/5000696_network_online_social media_social network_logo_icon.png"),
           (@"/Images/Icons/5000712_gmail_google_network_online_social media_icon.png"),
           (@"/Images/Icons/5172567_heart_like_love_icon.png"),
           (@"/Images/Icons/5296501_linkedin_network_linkedin logo_icon.png"),
           (@"/Images/Icons/5296504_forum_reddit_reddit logo_icon.png"),
           (@"/Images/Icons/5296519_video_vimeo_vimeo logo_icon.png"),
           (@"/Images/Icons/5296520_bubble_chat_mobile_whatsapp_whatsapp logo_icon.png"),
           (@"/Images/Icons/6636603_instagram_social media_social network_icon.png"),     
           (@"/Images/Icons/940991_tube_you_youtube_yt icon_icon.png"),           
        };

        public static string? GetBackgroundImage()
        {
            Dictionary<int, string> BackgroundResource = new Dictionary<int, string>();
            string? background;

            BackgroundResource.Add(12, @"Back02");
            BackgroundResource.Add(1, @"Back02");
            BackgroundResource.Add(2, @"Back02");
            BackgroundResource.Add(3, @"Back04");
            BackgroundResource.Add(4, @"Back04");
            BackgroundResource.Add(5, @"Back04");
            BackgroundResource.Add(6, @"Back05");
            BackgroundResource.Add(7, @"Back05");
            BackgroundResource.Add(8, @"Back05");
            BackgroundResource.Add(9, @"Back03");
            BackgroundResource.Add(10, @"Back03");
            BackgroundResource.Add(11, @"Back03");
            BackgroundResource.Add(0, @"Back01");
            BackgroundResource.Add(25, @"Backp");

            if (DateTime.Now.Month == 10 && DateTime.Now.Day == 25)
            {
                BackgroundResource.TryGetValue(25, out background);
            }
            else
            {                
                BackgroundResource.TryGetValue(DateTime.Now.Month, out background);
            }
            
            return background;
        }

        public static BitmapImage GetBitmapImageFromUri(string url)
        {
            Uri resourceUri = new Uri(url, UriKind.RelativeOrAbsolute);
            return new BitmapImage(resourceUri);            
        }        
    }
}
