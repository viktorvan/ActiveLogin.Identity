﻿using System;

namespace ActiveLogin.Identity.Swedish
{
    internal static class SwedishPersonalIdentityNumberDateCalculations
    {
        public static DateTime GetDateOfBirth(int year, int month, int day)
        {
            return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
        }

        public static int GetAge(DateTime dateOfBirth, DateTime date)
        {
            var age = date.Year - dateOfBirth.Year;

            if (date.Month < dateOfBirth.Month ||
                (date.Month == dateOfBirth.Month && date.Day < dateOfBirth.Day))
            {
                age -= 1;
            }

            if (age < 0)
            {
                throw new Exception("The person is not yet born.");
            }

            return age;
        }
    }
}