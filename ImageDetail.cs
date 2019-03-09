using System;
using System.Runtime.Serialization;

namespace Imgup
{
    [DataContract]
    class ImageDetail
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "link")]
        public string Link { get; set; }
        [DataMember(Name = "deletehash")]
        public string DeleteHash { get; set; }
        public DateTime UploadedOn { get; set; }
    }

    [DataContract]
    class ImgurResponse
    {
        [DataMember(Name = "data")]
        public ImageDetail data { get; set; }
    }
}