﻿namespace KQApi.Models.ProcessSharePoint.Entities
{
    public class AccessToken
    {
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string ext_expires_in { get; set; }
        public string not_before { get; set; }
        public string resource { get; set; }
        public string access_token { get; set; }
    }
}