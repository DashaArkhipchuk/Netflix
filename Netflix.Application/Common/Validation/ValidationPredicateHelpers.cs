using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Netflix.Application.Common.Validation
{
    internal static class ValidationPredicateHelpers
    {
        public static bool BeNotNullOrWhitespace(string? value) => !string.IsNullOrWhiteSpace(value);

        public static bool BeAValidRange(string range)
        {
            // Regex for valid ranges: "18-25", "under 18", "18+" case insensivive
            string rangePattern = @"^(?i)(under \d+|\d+[\-]\d+|\d+\+)$";
            return Regex.IsMatch(range, rangePattern);
        }

        public static bool BeAValidUrl(string? url) => Uri.IsWellFormedUriString(url, UriKind.Absolute);

        public static async Task<bool> BeAValidImageUrlAsync(string? url, HttpClient httpClient)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return false;

            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            using var response = await httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead);

            return response.IsSuccessStatusCode &&
                   response.Content.Headers.ContentType?.MediaType
                       ?.StartsWith("image/", StringComparison.OrdinalIgnoreCase) == true;
        }

        public static bool BeAValidPhoneNumber(string? phoneNumber)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var number = phoneNumberUtil.Parse(phoneNumber, null);
            return phoneNumberUtil.IsValidNumber(number);
        }
    }
}
