using System;

namespace SportLife.Website.Helpers.Converters {
    public class ImagePathConverter {
        private static readonly string PathToAvatars = "../Media/Avatars/UserAvatar_";

        public object ConvertUserAvatar ( object value  ) {
            var username = (string) value;
            return new Uri( $@"{PathToAvatars}{username}.jpeg" );
        }
    }
}