$(function () {
    //body加载时，从本地缓存中取帐户信息
    //debugger;
    getAccount();
    //alert(getSysInfo());
    //检查注册
    //checkRegister();
    // $('input, textarea').placeholder();
    //为form的ajax提交准备参数
    var options = {
        //target: '#outputdiv',//不能启用target，否则将导致提示框不显示关闭按钮，已改到responseText方法中处理
        //beforeSubmit: showRequest,

        success: showResponse
    };
    //debugger;
    //为防止表单重复提交，将type=submit必为type=button
    $('#btnSubmit').on('click', function () {

        //alert(123);
        //if (checkRegister())
        $('#slick-login').ajaxSubmit(options);
    });

    //响应回车键
    document.onkeydown = function (e) {

        if (!e) e = window.event; //火狐中是 window.event
        if ((e.keyCode || e.which) == 13) {
            //debugger;
            //var btnSubmit = document.getElementById("btnSubmit")
            $('#btnSubmit').triggerHandler("click");
            //btnSubmit.focus(); //让另一个控件获得焦点就等于让文本输入框失去焦点
            //btnSubmit.click();
            return false;
        }
    }
    //});

    //form ajax提交回调函数
    function showResponse(responseText, statusText) {
        //alert(statusText);
        //debugger;
        $('#outputdiv').fadeIn(1000);
        $('#outputdiv').html("<strong>" + responseText.Message + "</strong>");
        responseText.ErrorCount = 0;
        if (responseText.ErrorCount == 0) {
            
            $('#outputdiv').attr("class", "alert alert-success alert-dismissible");
            //if ($('#btnSaveAccount')[0].checked) {
            //    saveAccount();
            //}
            $('#outputdiv').fadeOut(3000);
            //alert(responseText.RoleName);
            //window.location.href = "index.html";
            setTimeout(window.location.href = "index.html?roleid='" + responseText.RoleID + "'&accountname='" + responseText.AccountName + "'&rolename='" + responseText.RoleName + "'", 2000);
            //'?roleid='""+responseText.RoleID+'""'
        } else {
            $('#outputdiv').attr("class", "alert alert-danger alert-dismissible");
            $('#outputdiv').fadeOut(3000);
        }
    }

    ////系统信息获取
    //function getSysInfo() {
    //    //debugger;
    //    var locator = new ActiveXObject("WbemScripting.SWbemLocator");
    //    var service = locator.ConnectServer(".");
    //    //CPU信息
    //    var cpu = new Enumerator(service.ExecQuery("SELECT * FROM Win32_Processor")).item();
    //    var cpuType = cpu.Name, hostName = cpu.SystemName;
    //    var cpuid = cpu.ProcessorID;
    //    //主板信息
    //    var board = new Enumerator(service.ExecQuery("SELECT * FROM Win32_BaseBoard")).item();
    //    var boardid = board.SerialNumber;
    //    var boardManufacturer = board.Manufacturer;
    //    //硬盘信息
    //    var media = new Enumerator(service.ExecQuery("SELECT * FROM Win32_PhysicalMedia")).item();
    //    var mediaid = media.SerialNumber;
    //    var regInfo = new Object();
    //    regInfo.cpuid = "" + cpuid + "";
    //    regInfo.boardid = "" + boardid + "";
    //    regInfo.boardManufacturer = "" + boardManufacturer + "";
    //    regInfo.mediaid = "" + trim(mediaid) + "";
    //    //var info = "{cpuid:" + cpuid + ",boardid:" + boardid + ",board Manufacturer:" + boardManufacturer
    //    //+ ",mediaid:" + trim(mediaid) + "}";
    //    return regInfo;
    //    //内存信息
    //    //var memory = new Enumerator(service.ExecQuery("SELECT * FROM Win32_PhysicalMemory"));
    //    //var memid;
    //    //for (var mem = [], i = 0; !memory.atEnd() ; memory.moveNext())
    //    //    memid += "内存序列号：" + mem.SerialNumber + "，内存厂商：" + mem.Manufacturer;
    //    //    mem[i++] = { cap: memory.item().Capacity / 1024 / 1024, speed: memory.item().Speed }
    //    // //系统信息
    //    // var system=new Enumerator (service.ExecQuery("SELECT * FROM Win32_ComputerSystem")).item();
    //    // var physicMenCap=Math.ceil(system.TotalPhysicalMemory/1024/1024),curUser=system.UserName,cpuCount=system.NumberOfProcessors

    //    //return {cpuType:cpuType,cpuCount:cpuCount,hostName:hostName,curUser:curUser,memCap:physicMenCap,mem:mem}
    //}

    //去除字符串中的空格
    function trim(t) {
        return (t || "").replace(/^\s+|\s+$/g, "");
    }

    //$('#btnReg').on('click', function () {
    //    //debugger;
    //    //alert(123);
    //    modalRegAlert();
    //});

    ////显示轮换图
    //$('#onebyone_slider').oneByOne({
    //    className: 'oneByOne1',
    //    easeType: 'random',
    //    slideShow: true,
    //    delay: 2000,
    //    slideShowDelay: 10000
    //});

    ////获取注册信息弹窗
    //function modalRegAlert() {
    //    var form = $('<form id="event_form">' +
    //            '<div class="form-group has-success has-feedback">' +
    //            '<label">您的注册信息如下：</br>' +
    //           JSON.stringify(getSysInfo()) +
    //            '</br>请联系翼聚源售后工作人员，将以上信息发给他们，帮您完成注册，谢谢！</label>' +
    //            '</div>' +
    //            '</form>');
    //    //debugger;
    //    //LoadAjaxContent('OA/AttendanceMonthStatistical/Create');
    //    var buttons = $('<button type="submit" id="event_submit" class="btn btn-primary btn-label-left pull-right">' +
    //                    '<span><i class="fa fa-clock-o"></i></span>' +
    //                    '确定' +
    //                    '</button>');
    //    OpenModalBox('系统提示', form, buttons);
    //    $('#event_submit').on('click', function () {
    //        CloseModalBox();
    //    });
    //}

    ////未注册时的弹窗
    //function modalUnRegAlert() {
    //    var form = $('<form id="event_form">' +
    //            '<div class="form-group has-success has-feedback">' +
    //            '<label">您当前使用的版本未注册，请按以下步骤操作，完成注册过程：</br>' +
    //            '1、关闭本窗口，通过登录页面上的【注册我的产品】按钮，取到注册信息，<b>然后<span style="color:red;">复制</span>出来</b></br>' +
    //            '2、联系翼聚源售后工作人员，帮您完成注册，谢谢！</br>' +
    //            '3、注册完成后，直接刷新本页面，登录即可正常使用</label>' +
    //            '</div>' +
    //            '</form>');
    //    //debugger;
    //    //LoadAjaxContent('OA/AttendanceMonthStatistical/Create');
    //    var buttons = $('<button type="submit" id="event_submit" class="btn btn-primary btn-label-left pull-right">' +
    //                    '<span><i class="fa fa-clock-o"></i></span>' +
    //                    '好的，我知道了' +
    //                    '</button>');
    //    OpenModalBox('系统提示', form, buttons);
    //    $('#event_submit').on('click', function () {
    //        CloseModalBox();
    //    });
    //    //$('#event_submit').on('click', function () {

    //    //    $.ajax({
    //    //        mimeType: 'text/html; charset=utf-8', // ! Need set mimeType only when run from local file
    //    //        url: url,
    //    //        type: 'GET',
    //    //        success: function (data) {
    //    //            //$('#ajax-content').html(data);
    //    //            alert(data);
    //    //            $('.preloader').hide();
    //    //        },
    //    //        error: function (jqXHR, textStatus, errorThrown) {
    //    //            alert(errorThrown);
    //    //        },
    //    //        dataType: "html",
    //    //        async: false
    //    //    });
    //    //    CloseModalBox();
    //    //});
    //}

    ////检查注册信息
    //function checkRegister() {
    //    //debugger;
    //    var result = false;
    //    var params = getSysInfo();
    //    url = "OA/Account/CheckClientRegistion";
    //    $.ajax({
    //        mimeType: 'text/html; charset=utf-8', // ! Need set mimeType only when run from local file
    //        url: "OA/Account/CheckClientRegistion",
    //        type: 'post',
    //        data: { collection: JSON.stringify(params) },
    //        success: function (data) {
    //            //$('#ajax-content').html(data);
    //            //alert(data);
    //            //$('.preloader').hide();
    //            //测试
    //            //debugger;
    //            //data = -1;
    //            //debugger;
    //            if (data < 1) {
    //                //lock();
    //                modalUnRegAlert();
    //                $('#btnReg').css("display", "block");
    //                result = false;
    //            } else {
    //                $('#btnReg').css("display", "none");
    //                result = true;
    //            }
    //        },
    //        error: function (jqXHR, textStatus, errorThrown) {
    //            //debugger;
    //            alert(errorThrown);
    //            result = false;
    //        },
    //        dataType: "html",
    //        async: false
    //    });
    //    return result;
    //}

    ////检测到产品未注册时，锁定登录界面(未采用专门的锁屏界面)
    //function lock() {
    //    //e.preventDefault();
    //    $('body').addClass('body-screensaver');
    //    $('#screensaver').addClass("show");
    //    ScreenSaver();
    //}

    //向缓存中存储帐户信息
    function saveAccount() {
        localStorage.setItem("username", $('#username').val());
        localStorage.setItem("password", $('#password').val());
    }

    //从缓存中取帐户信息
    function getAccount() {
        $('#username').val(localStorage.getItem("username"));
        $('#password').val(localStorage.getItem("password"));
    }

    //锁定屏幕上的注册按钮mouseover事件
    //$('#screen_unlock_registion').on('click', function () {
    //    var header = 'Enter current username and password';
    //    var form = $('<div class="form-group"><label class="control-label">Username</label><input type="text" class="form-control" name="username" /></div>' +
    //                '<div class="form-group"><label class="control-label">Password</label><input type="password" class="form-control" name="password" /></div>');
    //    var button = $('<div class="text-center"><a href="index.html" class="btn btn-primary">Unlock</a></div>');
    //    OpenModalBox(header, form, button);
    //});
})