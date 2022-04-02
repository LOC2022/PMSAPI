using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LOC.PMS.Model
{
    [Flags]
    public enum PalletAvailability
    {
        Ideal = 1,
        Active = 2,
        InActive = 3,
        OnHold = 4,
        Repair = 5
    }

    [Flags]
    public enum PalletStatus
    {
        [Display(Name = "Initial Scan", Description = "Initial scanning, Pallet gets mapped with the location.")]
        InitialScan = 1,

        [Display(Name = "Assigned", Description = "Pallet Assigined to day plan")]
        Assigned = 2,

        [Display(Name = "Picked", Description = "Picked from location against DO")]
        Picked = 3,

        [Display(Name = "Fixed Read", Description = "Fixed reader scanning.")]
        FixedRead = 4,

        [Display(Name = "Vendor Inward", Description = "Vendor Inward scanning against DC.")]
        VendorInward = 5,

        [Display(Name = "Vendor Dispatch", Description = "Vendor Dispatch Scanning.")]
        VendorDispatch = 6,

        [Display(Name = "CIPL Inward", Description = "CIPL Inward Scanning DC")]
        CIPLInward = 7,

        [Display(Name = "CIPL Dispatch", Description = "CIPL dispatch Scanning")]
        CIPLDispatch = 8,

        [Display(Name = "Warehouse Inward", Description = "Warehouse Inward RFID Reading against DC")]
        WarehouseInward = 9
    }

    [Flags]
    public enum OrderStatus
    {
        Open = 1,
        InTransit = 2,
        Completed = 3
    }


    public static class Extensions
    {

        public static string GetEnumDisplayName(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Name;
            else
                return value.ToString();
        }

        public static string GetDescription(Enum value)
        {
            var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var descriptionAttribute =
                enumMember == null
                    ? default(DescriptionAttribute)
                    : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return
                descriptionAttribute == null
                    ? value.ToString()
                    : descriptionAttribute.Description;
        }

    }
}