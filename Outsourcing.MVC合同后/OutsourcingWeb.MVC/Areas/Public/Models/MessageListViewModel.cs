using Outsourcing.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutsourcingWeb.MVC.Areas.Public.Models
{
    public class MessageListViewModel
    {
        public IPagedList<Requirement> Requirement { get; set; }
    }

    public class RequirementEditViewModel
    {
        public Requirement RequirementView { get; set; }
    }
}