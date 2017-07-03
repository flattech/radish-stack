window.back = window.back || {};
window.back.pages = window.back.pages || {};
window.back.pages.news = window.back.pages.news || {};
window.back.pages.programs = window.back.pages.programs || {};
window.back.pages.currentmodel = { ResourceId: '', ResourceTitle: '', Controller: '', Action: '', Url: '', TableName: '' };
window.back.finishCallbackForPopUp = function () { };
window.back.showForPopUp = function () { };
window.AppResources= {
    Next:'Next',  Yes: "Yes", No: 'No', YouDidAListOfChangesAreYouSureYouWantToLeaveWIthoutSave: 'لقد قمت بمجموعة تعديلات في هذه الصفحة من دون أن يتم حفظها. هل تريد الخروج من دون حفظها؟.',
}
window.back.PopUpTitle = "";
window.back.alert = function (message) {
    bootbox.alert(message);
};
window.back.confirm = function (message, callback, confirmlabel, cancellabel) {
    return bootbox.confirm(message, callback, confirmlabel || window.AppResources.Yes, cancellabel || window.AppResources.No);
};
window.back.formSubmitDelegate = function (e) {
    e.preventDefault();
    var $this = $(this);
    if ($this.find("[data-confirm=true][clicked=true]").length > 0) {
        bootbox.confirm($this.find("[data-confirm='true'][clicked=true]").data("confirmmessage"), function (result) {
            if (result)
                submitForm($this);
        });
    } else
        return submitForm($this);
    return false;
};
function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        //$.each(e.errors, function (key) {
        //   // if ('errors' in value) {
        //        $.each(this.errors, function () {
        //            message += this + "\n";
        //        });
        //   // }
        //});
        alert(e.errors);
    }
    this.cancelChanges();
}
window.addEventListener('popstate', function (event) {
    if (window.back.isDirty && window.back.isDirty())
        return false;
    if (event.state)
        window.back.loadUrl(document.location);
});
window.back.linkClickDelegate = function (e) {
    // alert(window.back.canLeave);
    var $this = $(this);
    //if ($this.data("posturl")) {
    //    blockUi();
    //    return true;
    //}
    var href = $this.attr("href");
    if (href === "javascript:;")
        return true;
    if ($this.is("[class^='k-']"))
        return true;
 
    if ($this.hasClass("sidemenu")) {
        var main = $(".sidemenuli");
        main.removeClass("active");
        main.removeClass("open");
        $this.parents("li.sidemenuli").addClass("open active");
   
      
    }

    if ($this.data("confirmmessage")) {
        e.preventDefault();
        bootbox.confirm($this.data("confirmmessage"), function (result) {
            if (result)
                window.back.loadUrl($this.data("href"), $this);
        });
        return true;
    }

    if ($this.data("imagepreview")) {
        e.preventDefault();
        $.colorbox({ href: href, scalePhotos: true, maxWidth: '100%', maxHeight: '100%' });
        return true;
    }
    if ($this.data("videopreview")) {
        e.preventDefault();
        if (href.indexOf("youtube.com") != -1)
            window.open(href);
        else
            $.colorbox({
                html: "<video width='400' height='300' controls autoplay><source src='"
                    + href + "' type='video/mp4'>Your browser does not support the video tag.</video>"
            });
        return true;
    }
    if (!href || href && (href.indexOf("#") != -1)) {
        if ($this.data("tabajaxurl") && !$this.data("loaded")) { //for ajax tabs
            $.get($this.data("tabajaxurl")).done(function (data) {
                if ($this.data("appendto"))
                    $(href).append(data);
                else
                    $(href).html(data);
                $this.data("loaded", "true");
            });
        }
        return true;
    }
    if (href.indexOf("http") != -1 && href.indexOf("http") < 10)
        return true;
    e.preventDefault();
    if ($this.data("mdnindex"))
        href += "&index=" + $this.data("mdnindex");
    window.back.loadUrl(href, $this);
    return false;
};
window.back.confirmExit = function () {
    if (window.back.isDirty && window.back.isDirty())
        return window.AppResources.YouDidAListOfChangesAreYouSureYouWantToLeaveWIthoutSave; // "لقد قمت بمجموعة تعديلات في هذه الصفحة من دون أن يتم حفظها. هل تريد الخروج من دون حفظها؟.";

}

var request = false;
window.back.loadUrl = function (href, elem, panelrefresh) {

    function navigateTo(continueCallback, prefresh) {
        //if (!continueCallback) {
        //    request = false;
        //    return true;
        //}
        //if (elem&&!elem.data("dontdisable"))
        //    elem.attr('disabled', 'disabled');
        if (prefresh)
            elem.find(".well").prepend("<img src='/content-cdn/Images/lod.GIF' width='30px'>");

        if (!prefresh)
            blockUi();
        var settings = elem && elem.data("postbody") ? {
            //timeout: 20000,
            type: "POST", data: elem.data("postbody"),
            url: href
        } : {
            // timeout: 20000,
            type: "GET", url: href
        };
        $.ajax(settings).done(function (data) {
            if (elem && elem.data("refreshpage")) {
                window.location.reload();
                return true;
            }
            if (!prefresh)
                UnblockUi();
            request = false;
            if (elem && elem.hasClass("navlink"))
                setActiveNav(elem);
            if (elem && elem.hasClass("wellwithactions"))
                setWellNav(elem);
            if (!prefresh)
                window.back.finishLoading(data, elem, href);
            else {
                elem.html(data);
                CallJqueryPlugins();
            }
        }).fail(function (jqXHR, textStatus) {
            if (!panelrefresh)
                UnblockUi();
            request = false;
            if (textStatus == 'timeout')
                bootbox.alert("Timeout (check your internet connection)");
            else
                bootbox.alert("An Erro hass occured");
        });
        return true;
    }
    //if (request)
    //    return false;
    request = true;
    // SetFormFieldsFromEditors();
    // if (window.back.isDirty && window.back.isDirty()) {
    //    window.back.confirmExit(navigateTo);
    //} else {

    navigateTo(true, panelrefresh);
    //}

};
window.back.finishLoading = function (data, elem, href) {

    if (elem && elem.data("closeaftersubmit"))
        bootbox.hideAll();
    if (elem && elem.data("refreshpanel")) {
        bootbox.hideAll();
        $.gritter.add({
            title: 'تنبيه ',
            time: 10000,
            text: '<i class="icon-bell-alt icon-animated-bell"></i> ' + window.AppResources.SuccessfullySaved,
            class_name: 'gritter-center gritter-success ',
        });
        window.back.loadUrl(elem.data("refreshurl"), $(elem.data("refreshpanel")), true);
    }
    else if (elem && elem.data("container")) {
        if (elem.data("appendtoconainer"))
            $(elem.data("container")).append(data);
        else
            $(elem.data("container")).html(data);

        if (elem.data("removeparentifclick"))
            elem.parent().remove();

    } else if (elem && elem.data("loadtype") == "popup") {
        var b = bootbox.dialog({
            message: data,
            title: elem.data("popuptitle"),
            buttons: !elem.data("nobuttons") ? {
                success: {
                    label: elem.data("buttontitle") || window.AppResources.Next,
                    className: "btn-success",
                    callback: function () {
                        window.back.finishCallbackForPopUp();
                    }
                }
            } : {}
        });
        b.on("shown.bs.modal", function () {
            if ($(".bootbox-body").find("#mdn-crop-container").length > 0) {
                var image = $("#mdn-crop-container #original-image");
                image.attr("src", image.data("url"));
                image.load(function () {
                    $("#mdn-crop-container .upload-cropper").each(linkUp);
                });
            }
        });
    } else if ($(data).hasClass("login-box")) {
        bootbox.dialog({
            message: data,
            title: window.back.PopUpTitle,
            buttons: {}
        });
    }
    else {

        var pagecontent = $(".page-content");
        pagecontent.html(data);
        //pagecontent.css({
        //    opacity: '0.0'
        //})
        //       .html(data)
        //       .delay(100)
        //       .animate({
        //           opacity: '1.0'
        //       }, 300);
        // pagecontent.find("#backbtn").attr("href", location.href);
        if ($(elem)) {
            var pageheader = pagecontent.find(".page-title");
            if ($(elem).data("dontchangeurl") == undefined && href)
                window.history.pushState("mayadeenlink", href, href);
            else if ($(elem).data("redirecturl") != undefined)
                window.history.pushState("mayadeenlink", $(elem).data("redirecturl"), $(elem).data("redirecturl"));
            else if (pageheader.data("url"))
                window.history.pushState("mayadeenlink", pageheader.data("url"), pageheader.data("url"));
            //if (pageheader.data("calljqueryplugins"))
            //    CallJqueryPlugins();
            var alertMessage = pagecontent.find(".notification");
            if (alertMessage.length) {
                var message = alertMessage.find("strong").text();
                $.gritter.add({
                    title: "Notification",
                    time: 3000,
                    text: '<i class="icon-bell-alt icon-animated-bell"></i> ' + message,
                    class_name: alertMessage.hasClass("alert-warning") ? 'gritter-center gritter-success ' : 'gritter-center gritter-error  ',
                });
            }
        }
        // window.back.pages.initialize();
    }
    Metronic.initAjax();

};
window.back.pages.initialize = function () {
    window.back.isDirty = function () {
        return false;
    };
    // window.back.pages.initializesearch();
    CallJqueryPlugins();
};
$(function () {
    $(window).bind("beforeunload", window.back.confirmExit);
    window.headerloader = document.getElementById("headerloader");
    window.headerloader.style.display = "none";
    $(document).on('click', 'input:submit', function (e) {
        var $this = $(this);
        //$this.attr('disabled', 'disabled');
        $this.parents("form:first").append('<input type=hidden name=' + e.target.name + ' />');
        $("input[type=submit]", $this.parents("form")).removeAttr("clicked");
        $this.attr("clicked", "true");
    });
    $(document).on('click', 'button:submit', function (e) {
        //var $this = $(this);
        //$this.attr('disabled', 'disabled');   return true;
        ////    //$this.parents("form:first").append('<input type=hidden name=' + e.target.name + ' />');
        ////    //$("input[type=submit]", $this.parents("form")).removeAttr("clicked");
        ////    //$this.attr("clicked", "true");
    });
    // $(".chosen-select").chosen();
    window.blockUiloader = $("#busy-holder");
    $(document).ajaxStart(function () {
        window.headerloader.style.display = "inline-block";
    }).ajaxStop(function () {
        window.headerloader.style.display = "none";
    });
    $(document).on("click", 'a:not(.notajax)', window.back.linkClickDelegate);
    $(document).on('submit', 'form:not(.notajax)', window.back.formSubmitDelegate);
    //$(document).on('keypress', 'form:not(.notajax)', function (e) {
    //    if (e.keyCode == 13) {
    //        e.preventDefault();
    //    }
    //});
    $.postJSON = function (url, data) {
        var o = {
            url: url,
            type: "POST",
            dataType: "json",
            contentType: 'application/json; charset=utf-8'
        };

        if (data !== undefined) {
            o.data = JSON.stringify(data);
        }

        return $.ajax(o);
    };
});
function submitForm($this) {
    SetFormFieldsFromEditors();
    //if (!$this.data("dontdisablesbuttons"))
    //    $this.find(":submit").attr('disabled', 'disabled');
    //$.post('/Audit/SaveAudit', window.back.pages.currentmodel);
    blockUi();
    $.ajax({
        url: $this.attr("action"),
        data: $this.serialize(),
        type: $this.attr("method") || "POSt",
        dataType: "html"//, timeout: 10000,
    }).done(function (data) {
        UnblockUi();
        window.back.finishLoading(data, $this);
    }).fail(function (jqXHR, textStatus) {
        UnblockUi();
        if (!$this.data("dontdisablesbuttons"))
            $this.find(":submit").removeAttr('disabled');
        if (textStatus == 'timeout')
            window.back.alert("Timeout (check your internet connection)");
        else
            window.back.alert(jqXHR.statusText);
    });
    return false;
}
function CallJqueryPlugins() {


}
function blockUi() {
    //centerDiv("#busy");

    window.blockUiloader.fadeIn("fast");
}
function UnblockUi() {
    window.blockUiloader.fadeOut("fast");
}
function inititializeDateTimePickers() {
    if ($('.mayadeendatetimepicker').length > 0)
        $('.mayadeendatetimepicker').datetimepicker();
    if ($('.mayadeendatetimepickerinline').length > 0)
        $('.mayadeendatetimepickerinline').datetimepicker();//{ widgetParent: "#widgetparent" }

    $('.mayadeentimepicker').datetimepicker({
        pickDate: false
    });
}
function inititializeEditors() {
    var editors = $('.mdn-editor');
    if (editors.length > 0)
        editors.summernote({
            height: 300,   //set editable area's height
            focus: true    //set focus editable area after Initialize summernote
        });
}


function SetFormFieldsFromEditors() {
    if ($('.note-editable').length > 0) {
        $('.note-editable').each(function (index, element) {
            $(element).parent().next("input[type=hidden]").val($(element).html());
        });
    }
}
function setActiveNav(element) {
    $("ul.nav-list li").removeClass("active");
    if (element.attr("href") != "#") {
        element.parent().addClass("active");
    }
}
function setWellNav(element) {
    $("#wells .wellwithactions").removeClass("active");
    element.addClass("active");
}