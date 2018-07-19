﻿using System;

namespace App.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}