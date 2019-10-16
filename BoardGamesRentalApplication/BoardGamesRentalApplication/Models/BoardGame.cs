﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGamesRentalApplication.Models
{
    public class BoardGame
    {
        public int BoardGameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }
        public int PlayerCount { get; set; }
        public int MinimumAge { get; set; }
        public string BoardGamePublisherName { get; set; }
        public string BoardGameStateName { get; set; }
    }
}