using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutsourcingWeb.MVC.Areas.Tenet.Controllers
{

    /// <summary>
    /// 服务宗旨的几个页面展示
    /// </summary>
    /// <returns></returns>
    public class TenetController : Controller
    {

        /// <summary>
        /// 交易可信页面的显示
        /// </summary>
        /// <returns></returns>
        // GET: Tenet/Tenet
        public ActionResult Trade()
        {
            return View();
        }
        /// <summary>
        /// 精准匹配页面的显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Matching()
        {
            return View();
        }

        /// 权益保障页面的显示
        public ActionResult Protection()
        {
            return View();
        }

        /// 可靠支付页面的显示
        public ActionResult Reliable()
        {
            return View();
        }

        /// 服务专业页面的显示
        public ActionResult Professional()
        {
            return View();
        }


        /// 企业需求页面的显示
        public ActionResult Enterprise()
        {
            return View();
        }

        ///人才甄选页面的显示
        public ActionResult Selection()
        {
            return View();
        }

        ///人才委派页面的显示
        public ActionResult appoint()
        {
            return View();
        }

        ///企业面试页面的显示
        public ActionResult interview()
        {
            return View();
        }
        ///签订合同页面的显示
        public ActionResult Sign()
        {
            return View();
        }

        ///人员到岗页面的显示
        public ActionResult hillock()
        {
            return View();
        }



        ///案例展示1页面的显示
        public ActionResult Case()
        {
            return View();
        }


        ///案例展示更多页面的显示
        public ActionResult More()
        {
            return View();
        }

        ///案例展示详细页面的显示
        public ActionResult detailed()
        {
            return View();
        }

        ///banner1详细的显示
        public ActionResult Bn1()
        {
            return View();
        }
        ///banner2详细的显示
        public ActionResult Bn2()
        {
            return View();
        }

        ///banner3详细的显示
        public ActionResult Bn3()
        {
            return View();
        }


    }
}