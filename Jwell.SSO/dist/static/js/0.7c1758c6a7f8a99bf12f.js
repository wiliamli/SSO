webpackJsonp([0],{mvHQ:function(e,t,r){e.exports={default:r("qkKv"),__esModule:!0}},q3Wa:function(e,t,r){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var n=r("mvHQ"),o=r.n(n),a=r("//Fk"),i=r.n(a),s=r("mtWM"),c=r.n(s);function l(e){return new i.a(function(t,r){c.a.create({headers:{"Content-Type":"application/json"}})(e).then(function(e){t(e)}).catch(function(e){r(e)})})}var u={baseUrl:"http://10.0.100.201/SSO"};var m={data:function(){var e=/[`~!@#$%^&*()_+<>?:"{},.\/;'[\]]/im,t=/[`~!@#$%^&*()_+<>?:"{},;'[\]]/im,r=/[·！#￥（——）：；“”‘、，|《。》？、【】[\]]/im,n=/[\u4e00-\u9fa5]/gi;return{regCard:!0,loading:!1,formReg:{ServiceNumber:"",ServiceSign:"",AccessToken:"",RedirectUri:"",TeamLeader:"",DomainName:""},hintTitle:"",hintContent:"",hintTip:"",ruleReg:{ServiceNumber:[{required:!0,validator:function(t,o,a){if(""===o)return a(new Error("请输入服务编号"));setTimeout(function(){e.test(o)||r.test(o)||n.test(o)?a(new Error("服务号只能输入数字和字母")):a()},500)}}],ServiceSign:[{required:!0,validator:function(e,o,a){if(""===o)return a(new Error("请输入服务标识"));setTimeout(function(){t.test(o)||r.test(o)||n.test(o)?a(new Error("服务标识只能输入数字和字母")):a()},500)}}],AccessToken:[{required:!0,validator:function(t,o,a){if(""===o)return a(new Error("请输入AccessToken"));setTimeout(function(){e.test(o)||r.test(o)||n.test(o)?a(new Error("AccessToken只能输入数字和字母")):a()},500)}}],RedirectUri:[{required:!0,validator:function(e,o,a){if(""===o)return a(new Error("请输入回调地址"));setTimeout(function(){t.test(o)||r.test(o)||n.test(o)?a(new Error("回调地址只能输入数字和字母")):a()},500)}}],TeamLeader:[{required:!0,validator:function(t,o,a){if(""===o)return a(new Error("请输入项目组Leader"));setTimeout(function(){e.test(o)||r.test(o)||n.test(o)?a(new Error("项目组Leader工号只能输入数字和字母")):a()},500)}}],DomainName:[{required:!0,validator:function(e,o,a){if(""===o)return a(new Error("请输入域名"));setTimeout(function(){t.test(o)||r.test(o)||n.test(o)?a(new Error("域名只能输入数字和字母")):a()},500)}}]}}},methods:{handleSubmit:function(e){var t=this;this.loading=!0,this.$refs[e].validate(function(e){var r,n;e?(r="/OAuthManage/Save",n=t.formReg,l({url:u.baseUrl+r,method:"post",data:n})).then(function(e){console.log("成功",e),e.data.Success?(t.$Message.success("注册成功!"),t.hintTitle="注册成功",t.hintContent="这是您的秘钥："+e.data.Data.ClientSecret+"。",t.hintTip="请妥善保管。若有遗失，请联系管理员获取。"):(t.$Message.error("注册失败!"),t.hintTitle="注册失败",t.hintContent=e.data.Message||"系统错误，请联系管理员"),t.regCard=!1}).catch(function(e){console.log("失败",e)}):(t.$Message.error("请输入正确的信息!"),t.loading=!1)})},handleReset:function(e){this.$refs[e].resetFields()}},beforeCreate:function(){var e,t,r=this;(e="/OAuthManage/Register",l({url:u.baseUrl+e,method:"get",params:t})).then(function(e){console.log("成功",e)}).catch(function(e){var t,n;if(console.log("失败",e),o()(e).includes("401")){var a=window.location.href;a=a.replace(/\#/g,"~"),(t="/Default/LoginUrl",n={returnUrl:a},l({url:u.baseUrl+t,method:"get",params:n})).then(function(e){console.log("成功",e.data),window.location.href=e.data}).catch(function(e){r.$Message.error("sso登录失败")})}else r.$Message.error(e)})}},d={render:function(){var e=this,t=e.$createElement,r=e._self._c||t;return r("div",{staticClass:"card",class:{cardReg:e.regCard}},[r("div",{staticClass:"inTitle"},[e._v("\n    注册\n  ")]),e._v(" "),e.regCard?r("Form",{ref:"formReg",staticClass:"formMain",attrs:{model:e.formReg,rules:e.ruleReg,"label-width":100}},[r("FormItem",{attrs:{label:"服务编号",prop:"ServiceNumber"}},[r("Input",{attrs:{placeholder:"请输入服务编号"},model:{value:e.formReg.ServiceNumber,callback:function(t){e.$set(e.formReg,"ServiceNumber",t)},expression:"formReg.ServiceNumber"}})],1),e._v(" "),r("FormItem",{attrs:{label:"服务标识",prop:"ServiceSign"}},[r("Input",{attrs:{placeholder:"请输入服务标识"},model:{value:e.formReg.ServiceSign,callback:function(t){e.$set(e.formReg,"ServiceSign",t)},expression:"formReg.ServiceSign"}})],1),e._v(" "),r("FormItem",{attrs:{label:"AccessToken",prop:"AccessToken"}},[r("Input",{attrs:{placeholder:"请输入AccessToken"},model:{value:e.formReg.AccessToken,callback:function(t){e.$set(e.formReg,"AccessToken",t)},expression:"formReg.AccessToken"}})],1),e._v(" "),r("FormItem",{attrs:{label:"回调地址",prop:"RedirectUri"}},[r("Input",{attrs:{placeholder:"请输入回调地址"},model:{value:e.formReg.RedirectUri,callback:function(t){e.$set(e.formReg,"RedirectUri",t)},expression:"formReg.RedirectUri"}})],1),e._v(" "),r("FormItem",{attrs:{label:"项目组Leader",prop:"TeamLeader"}},[r("Input",{attrs:{placeholder:"请输入项目组Leader"},model:{value:e.formReg.TeamLeader,callback:function(t){e.$set(e.formReg,"TeamLeader",t)},expression:"formReg.TeamLeader"}})],1),e._v(" "),r("FormItem",{attrs:{label:"域名",prop:"DomainName"}},[r("Input",{attrs:{placeholder:"请输入域名"},model:{value:e.formReg.DomainName,callback:function(t){e.$set(e.formReg,"DomainName",t)},expression:"formReg.DomainName"}})],1),e._v(" "),r("FormItem",{staticClass:"regBtn"},[r("Button",{attrs:{type:"primary",loading:e.loading},on:{click:function(t){e.handleSubmit("formReg")}}},[e.loading?r("span",[e._v("加载中...")]):r("span",[e._v("注册")])]),e._v(" "),r("Button",{on:{click:function(t){e.handleReset("formReg")}}},[e._v("取消")])],1)],1):e._e(),e._v(" "),e.regCard?e._e():r("Card",{attrs:{bordered:!1,"dis-hover":""}},[r("p",{attrs:{slot:"title"},slot:"title"},[e._v(e._s(e.hintTitle))]),e._v(" "),r("p",[e._v("\n        "+e._s(e.hintContent)+"\n        "),r("br"),e._v(" "),r("span",{staticClass:"hintMes"},[e._v("\n          "+e._s(e.hintTip)+"\n        ")])])])],1)},staticRenderFns:[]},f=r("VU/8")(m,d,!1,null,null,null);t.default=f.exports},qkKv:function(e,t,r){var n=r("FeBl"),o=n.JSON||(n.JSON={stringify:JSON.stringify});e.exports=function(e){return o.stringify.apply(o,arguments)}}});
//# sourceMappingURL=0.7c1758c6a7f8a99bf12f.js.map