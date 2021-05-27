using System;

namespace Catalog.API.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }        
        public DateTime CreatedAt { get; set; }
        public DateTime? DisabledAt { get; set; }
    }
}