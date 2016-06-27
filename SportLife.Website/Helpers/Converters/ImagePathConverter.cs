using System;

namespace SportLife.Website.Helpers.Converters {
    public static class ImagePathConverter {
        private static readonly string PathToAvatars = "../Media/Avatars/UserAvatar_";

        public static Uri ConvertUserAvatar ( string username  ) {
            return new Uri( $@"{PathToAvatars}{username}.jpeg" );
        }
    }
}