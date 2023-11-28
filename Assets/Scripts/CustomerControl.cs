using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerControl : MonoBehaviour
{
    [SerializeField] private NightSpecifications currentNightSpecifications;
    [SerializeField] private Customer currentCustomerToControl;

    public NightSpecifications CurrentNightSpecifications { get => currentNightSpecifications; set => currentNightSpecifications = value; }
    public Customer CurrentCustomerToControl { get => currentCustomerToControl; set => currentCustomerToControl = value; }

    public struct Date
    {
        public int day;
        public int month;
        public int year;

        public Date(int day, int month, int year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }

        // Custom method to check if one date is greater than another
        public static bool operator >(Date date1, Date date2)
        {
            if (date1.year > date2.year)
                return true;
            else if (date1.year < date2.year)
                return false;

            if (date1.month > date2.month)
                return true;
            else if (date1.month < date2.month)
                return false;

            return date1.day > date2.day;
        }

        // Custom method to check if one date is less than another
        public static bool operator <(Date date1, Date date2)
        {
            return !(date1 > date2) && !(date1 == date2);
        }

        // Custom method to check if two dates are equal
        public static bool operator ==(Date date1, Date date2)
        {
            return date1.day == date2.day && date1.month == date2.month && date1.year == date2.year;
        }

        // Custom method to check if two dates are not equal
        public static bool operator !=(Date date1, Date date2)
        {
            return !(date1 == date2);
        }
    }


    public void UpdateCurrentNight(Night currentNight)
    {
        currentNightSpecifications = currentNight.NightSpecifications;
    }

    public bool ControlOneCustomer(Customer customer)
    {
        currentCustomerToControl = customer;

        if (!isDistrictAllowed(currentCustomerToControl.GetDistrictNumber))
        {
            return false;
        }
        else if (!isImplantAllowed(currentCustomerToControl.GetImplants, currentCustomerToControl.GetImplantsRegistered))
        {
            return false;
        }
        else if (!isInAge(currentCustomerToControl.GetAge))
        {
            return false;
        }
        else if (!isCriminal(currentCustomerToControl.GetCrimes, currentCustomerToControl.CustomerHasJustificant))
        {
            return false;
        }
        else if (isDocumentExpired(currentCustomerToControl.GetDocumentExpiryDate, NightSystem.Instance.CurrentDate))
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    private bool isCriminal(List<Crimes> crimes, bool hasJustificant)
    {
        bool noCrime = false, justifiedCrimes = false, unjustifiedCrimes = false, allowedCrimes = true; 
        int count = 0;

        foreach (Crimes crime in crimes)
        {
            foreach (Crimes allowed in currentNightSpecifications.SpecificationsPermitedCrimes)
            {
                count = 0;
                if (crime.CrimeName.Equals(allowed.CrimeName))
                {
                    count++;
                }
            }
            if (count == 0)
            {
                allowedCrimes = false;
            }
        }

        if (crimes.Count == 0)
        {
            noCrime = true;
        }

        if (crimes.Count > 0 && hasJustificant)
        {
            justifiedCrimes = true;
        }

        if (crimes.Count > 0 && !hasJustificant)
        {
            unjustifiedCrimes = true;
        }

        if (noCrime == currentNightSpecifications.SpecificationNoCrime == true)
        {
            return true;
        }
        else if (justifiedCrimes == currentNightSpecifications.SpecificationJustifiedCrime == true)
        {
            if (allowedCrimes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (unjustifiedCrimes == currentNightSpecifications.SpecificationUnjustifiedCrime == true)
        {
            if (allowedCrimes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    private bool isInAge(int age)
    {
        if (age >= currentNightSpecifications.SpecificationMinimumAge)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isImplantAllowed(List<Implant> getImplants, List<Implant> getImplantsRegistered)
    {
        bool noImplant = false, registeredImplant = false, unregisteredImplant = false, ilegalImplant = false, hasIlegalImplants = false;

        foreach (Implant implant in currentCustomerToControl.GetImplants)
        {
            if (!implant.IsLegal)
            {
                hasIlegalImplants = true;
            }
        }

        if (currentCustomerToControl.GetImplants.Count == 0)
        {
            noImplant = true;
        }

        if (currentCustomerToControl.GetImplants.Count == currentCustomerToControl.GetImplantsRegistered.Count)
        {
            registeredImplant = true;
        }

        if (currentCustomerToControl.GetImplants.Count >= currentCustomerToControl.GetImplantsRegistered.Count && !hasIlegalImplants)
        {
            unregisteredImplant = true;
        }

        if (currentCustomerToControl.GetImplants.Count >= currentCustomerToControl.GetImplantsRegistered.Count && hasIlegalImplants)
        {
            ilegalImplant = true;
        }


        if (noImplant == currentNightSpecifications.SpecificationNoImplants == true)
        {
            return true;
        }
        else if(registeredImplant == currentNightSpecifications.SpecificationRegisteredImplants == true)
        {
            return true;
        }
        else if(unregisteredImplant == currentNightSpecifications.SpecificationUnregisteredImplants == true)
        {
            return true;
        }
        else if (ilegalImplant == currentNightSpecifications.SpecificationIlegalImplants == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isDistrictAllowed(int customerDistrictNumber)
    {
        foreach (int districtNumber in currentNightSpecifications.SpecificationDistricNumber)
        {
            if (customerDistrictNumber == districtNumber)
            {
                return false;
            }
        }
        return true;
    }

    public bool isDocumentExpired(string expiryDateString, string gameDateString)
    {
        // Parse the expiry date string
        if (TryParseDate(expiryDateString, out Date expiryDate))
        {
            // Parse the game date string
            if (TryParseDate(gameDateString, out Date currentDate))
            {
                // Compare the expiry date with the current date
                if (expiryDate < currentDate)
                {
                    Debug.Log("Document has expired.");
                    return true;
                }
                else
                {
                    Debug.Log("Document is still valid.");
                    return false;
                }
            }
            else
            {
                Debug.LogError("Invalid date format. Please provide the date in DD/MM/YYYY format.");
                return false;
            }
        }
        else
        {
            Debug.LogError("Invalid expiry date format. Please provide the date in DD/MM/YYYY format.");
            return false;
        }
    }

    private bool TryParseDate(string dateString, out Date result)
    {
        result = new Date();

        string[] dateParts = dateString.Split('/');
        if (dateParts.Length == 3 && int.TryParse(dateParts[0], out result.day) && int.TryParse(dateParts[1], out result.month) && int.TryParse(dateParts[2], out result.year))
        {
            if (result.day > 31 && (result.month == 1 || result.month == 3 || result.month == 5 || result.month == 7 || result.month == 8 || result.month == 10 || result.month == 12))
            {
                return false;
            }
            else if (result.day > 30 && (result.month == 4 || result.month == 6 || result.month == 9 || result.month == 11))
            {
                return false;
            }
            else if (result.day > 28 && result.month == 2)
            {
                return false;
            }
            else if (0 > result.month && result.month > 12)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        else
        {
            return false;
        }
    }

}


