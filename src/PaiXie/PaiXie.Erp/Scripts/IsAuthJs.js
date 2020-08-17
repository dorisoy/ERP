//js 权限
//function   IsAuthJs(Code) {
//    var result=false;
//       $.ajax({
//           url: "/Base/GetIsAuthJs?ram=" + Math.random() + "&Code=" + Code,
//                    type: "GET",
//                    cache: false,
//                    async: false,
//                    success: function (r) {
//                        var map = $.parseJSON(r);
//                        if (map.result ==1) {
//                            result = true;
//                        }
//                        else
//                        {
//                            $.MsgBox.Alert("提示", map.message);
//                            result= false;
//                        }      
//                    },
//                    error: function () {
//                        result = false;
//                        //result = '{ "result": -99,  "message": "没有权限！" }';
//                    }
//                });
//                      return   result; 
//            }