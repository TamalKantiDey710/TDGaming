﻿namespace TDGaming.API.DTO
{
    public class VideoGameFilterDto
    {
        public string? Name { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
