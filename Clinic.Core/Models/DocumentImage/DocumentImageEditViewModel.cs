﻿using System;
using Clinic.Core.Models.Common;

namespace Clinic.Core.Models.DocumentImage
{
   public class DocumentImageEditViewModel : BaseViewModel
    {
        public Guid Id { get; set; }
        public  string FileName { get; set; }

    }
}
