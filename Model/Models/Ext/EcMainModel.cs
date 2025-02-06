using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models.Ext
{
    public class EcMainModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("company")]
        public EcIdNameType Company { get; set; }
        [JsonProperty("user")]
        public EcUserModel User { get; set; }
        [JsonProperty("truck")]
        public EcTruckModel Truck { get; set; }
        [JsonProperty("driver")]
        public EcDriver Driver { get; set; }
        [JsonProperty("pregate")]
        public EcIdNameType Pregate { get; set; }
        [JsonProperty("park")]
        public EcIdNameType Park { get; set; }
        [JsonProperty("terminal")]
        public EcIdNameType Terminal { get; set; }
        [JsonProperty("category")]
        public EcIdNameType Category { get; set; }
        [JsonProperty("port")]
        public EcIdNameType Port { get; set; }
        [JsonProperty("statuses")]
        public List<EcIdNameType> Statuses { get; set; }
        public string  StatusesHistory { get; set; }
        [JsonProperty("material")]
        public EcIdNameType Material { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("estimated_arrival_date")]
        public DateTime? EstimatedArrivalDate { get; set; }
        [JsonProperty("estimated_arrival_time")]
        public DateTime EstimatedArrivalTime { get; set; }
        /*[JsonProperty("departure_date")]
        public DateTime? DepartureDate { get; set; }*/
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

    }

    public class EcIdNameType
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        // Use this model for CommanyName, Pregate, Terminal, Category, Park, Port and Status   
    }

    public class EcUserModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

    }

    public class EcTruckModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("tag")]
        public string Tag { get; set; }
        [JsonProperty("plate_number")]
        public string PlateNumber { get; set; }
        [JsonProperty("chasis_number")]
        public string ChasisNumber { get; set; }
        [JsonProperty("mss_verified")]
        public bool MssVerified { get; set; }
        [JsonProperty("type")]
        public EcIdNameType Type { get; set; }
        [JsonProperty("make")]
        public EcIdNameType Make { get; set; }
        [JsonProperty("capacity")]
        public EcIdNameType Capacity { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("user")]
        public EcUserModel User { get; set; }
    }

    public class EcDriver
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("dob")]
        public DateTime DOB { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("driver_license")]
        public string DriverLicense { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("user")]
        public EcUserModel User { get; set; }

    }

}
