namespace AddressResolver
{
    using System;
    using System.Globalization;
    using System.Net;

    public class AddressResolvedResult
    {
        public IPAddress Address { get; }
        public bool IsReversedAddress { get; }
        public RegionInfo RegionInfo { get; }
        public StatusCode StatusCode { get; }

        public AddressResolvedResult(StatusCode statusCode, bool isReversedAddress, IPAddress address, RegionInfo regionInfo)
        {
            this.StatusCode = statusCode;
            this.IsReversedAddress = isReversedAddress;
            this.Address = address ?? throw new ArgumentNullException(nameof(address));
            this.RegionInfo = regionInfo ?? throw new ArgumentNullException(nameof(regionInfo));
        }

        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is AddressResolvedResult other))
                return false;

            return this.GetHashCode().Equals(other.GetHashCode());
        }

        public override int GetHashCode()
        {
            return this.Address.GetHashCode();
        }
    }
}