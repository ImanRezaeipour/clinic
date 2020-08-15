using System;

namespace Clinic.Service.Managers.Video
{
    public class VideoMetaData
    {
        public TimeSpan Duration { get; internal set; }

        public Video VideoData { get; internal set; }

        public Audio AudioData { get; internal set; }
    }
}
