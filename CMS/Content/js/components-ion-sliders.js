var ComponentsIonSliders = function () {

    return {
        //main function to initiate the module
        init: function () {

            $("#range_4").ionRangeSlider({
               // type: "double",
             //   grid: true,
              //  grid_num: 1,
                // from: 6.10, to: 7.45,
                from:0,
                keyboard:true,keyboard_step:1,
                grid: true, prettify: function (num) {
                   // var nn = (num + "").replace("6.", "").replace("7.", "");
                   // if (nn % 5 != 0)
                     //   return "";
                    if ((num + "").length == 3) {
                        return  (num + "").replace(".", ":") + "0";
                    }
                    return (num + "").replace(".", ":");//.replace("6:", ":").replace("7:", ":");
                },
                values: [6.00, 6.01, 6.02, 6.03, 6.04, 6.05, 6.06, 6.07, 6.08, 6.09, 6.10, 6.11, 6.12, 6.13, 6.14, 6.15, 6.16, 6.17, 6.18, 6.19, 6.20, 6.21, 6.22, 6.23, 6.24, 6.25, 6.26, 6.27, 6.28, 6.29, 6.30, 6.31, 6.32, 6.33, 6.34, 6.35, 6.36, 6.37, 6.38, 6.39, 6.40, 6.41, 6.42, 6.43, 6.44, 6.45, 6.46, 6.47, 6.48, 6.49, 6.50, 6.51, 6.52, 6.53, 6.54, 6.55, 6.56, 6.57, 6.58, 6.59, 7.00, 7.01, 7.02, 7.03, 7.04, 7.05, 7.06, 7.07, 7.08, 7.09, 7.10, 7.11, 7.12, 7.13, 7.14, 7.15, 7.16, 7.17, 7.18, 7.19, 7.20, 7.21, 7.22, 7.23, 7.24, 7.25, 7.26, 7.27, 7.28, 7.29, 7.30, 7.31, 7.32, 7.33, 7.34, 7.35, 7.36, 7.37, 7.38, 7.39, 7.40, 7.41, 7.42, 7.43, 7.44, 7.45, 7.46, 7.47, 7.48, 7.49, 7.50, 7.51, 7.52, 7.53, 7.54, 7.55, 7.56, 7.57, 7.58, 7.59, 8.00]
            });
         

            $("#updateLast").on("click", function(){

                $("#range_3").ionRangeSlider("update", {
                    min: Math.round(10000 + Math.random() * 40000),
                    max: Math.round(200000 + Math.random() * 100000),
                    step: 1,
                    from: Math.round(40000 + Math.random() * 40000),
                    to: Math.round(150000 + Math.random() * 80000)
                });

            });
            
        }

    };

}();