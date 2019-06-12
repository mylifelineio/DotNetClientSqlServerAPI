using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLifeline.DotNetClientSqlServerAPI.Models
{
    public enum EnumAlert { None, SMS, Email, Panic }
    public enum EnumGeoFencePosition { None, In, Out }
    public enum EnumSchedule { Remove, Silent, Alarm }
    public enum EnumGPSCycle { None, Precision, Unsupported, Interval, Manual, HighPrecision }
    public enum EnumContactType { Emergency, Other }
    public enum EnumDeviceState { Inactive, Initialize, Position, Panic, Heartbeat, LowBattery, Updated, Removed, GeoFence, Shutdown, Disconnected, EventInProgress, EventOnHold, ServerIPChange, ActiveTracker }
    public enum EnumDevicePosition { None, GPS, LBS, WIFI }

    public class DeviceLog
    {
        [Key]
        public Guid DeviceLogID {get;set;}
        [StringLength(17, MinimumLength = 11)]
        [Required]
        public string DeviceID { get; set; }
        [MaxLength(30)]
        public string Serial { get; set; }
        public string ProductID { get; set; }
        public EnumDeviceState DeviceState { get; set; }
        public EnumDevicePosition DevicePositionType { get; set; }
        public DateTime? LastHeartbeat { get; set; }
        public DateTime? LastPositionTime { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public float? LastLatitude { get; set; }
        public float? LastLongitude { get; set; }
        public int? Altitude { get; set; }
        public int? Speed { get; set; }
        public int? Direction { get; set; }
        public int? Pedometer { get; set; }
        public int? Satellites { get; set; }
        public int? Temperature { get; set; }
        public int? HeartRate { get; set; }
        public int? BatteryLevel { get; set; }
        public int? BatteryPerc { get; set; }
        public string CellTowers { get; set; }
        public string WifiAccessPoints { get; set; }
        public string MSISDN { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string UserImageUrl { get; set; }
        [StringLength(10)]
        public string UserPassword { get; set; }
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string UserCharacteristics { get; set; }
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string UserMedical { get; set; }
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string UserAddress { get; set; }
        [StringLength(1000)]
        public string ContactsEmergency { get; set; }
        public bool DonotCallToDevice { get; set; }
    }
}
