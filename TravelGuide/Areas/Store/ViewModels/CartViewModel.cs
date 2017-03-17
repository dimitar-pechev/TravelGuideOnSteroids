using System;
using System.Collections.Generic;
using System.Linq;
using TravelGuide.Common.Contracts;
using TravelGuide.Models;

namespace TravelGuide.Areas.Store.ViewModels
{
    public class CartViewModel : IMapFrom<User>
    {
        public IEnumerable<StoreItemCartViewModel> StoreItems { get; set; }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Total
        {
            get
            {
                if (this.StoreItems != null)
                {
                    return $"{this.StoreItems.Sum(x => x.Price)} BGN";
                }
                else
                {
                    return "0 BGN";
                }
            }
        }
    }
}