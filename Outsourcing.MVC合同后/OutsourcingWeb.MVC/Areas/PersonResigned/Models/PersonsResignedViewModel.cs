using Outsourcing.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutsourcingWeb.MVC.Areas.PersonResigned.Models
{
    public class PersonsResignedViewModel
    {
        public IPagedList<PersonsEntrySet> PersonsEntrySets { get; set; }
    }
}