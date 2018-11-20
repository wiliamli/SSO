new Vue({
    el: '#app',
    data() {
        let regEn = /[`~!@#$%^&*()_+<>?:"{},.\/;'[\]]/im,       //英文特殊字符
            regCn = /[·！#￥（——）：；“”‘、，|《。》？、【】[\]]/im,//中文特殊字符
            regZh = /[\u4e00-\u9fa5]/ig                         //中文字符
        //const validateCode = (rule, value, callback) => {
        //    if (value === '') {
        //        callback(new Error('请输入验证码'));
        //    } else {
        //        callback();
        //    }
        //};
        const validatePass = (rule, value, callback) => {
            if (value === '') {
                callback(new Error('请输入您的登录密码'));
            } else {
                callback();
            }
        };
        const validateJob = (rule, value, callback) => {
            if (!value) {
                return callback(new Error('请输入您的工号'));
            } 
             //模拟异步验证效果
            setTimeout(() => {
                if (regEn.test(value) || regCn.test(value) || regZh.test(value)){
                    callback(new Error('工号只能输入数字和字母'));
                } else {
                    callback()
                }
            }, 500);
        };
        return {
            formCustom: {
                EmployeeID: '',
                Password: '',
                valicode: ''
            },
            ruleCustom: {
                EmployeeID: [
                    { required: true, validator: validateJob, trigger: 'blur' }
                ],
                Password: [
                    { required: true, validator: validatePass, trigger: 'blur' }
                ],
                //valicode: [
                //    { validator: validateCode, trigger: 'blur' }
                //]
            }
        }
    },
    methods: {
        handleSubmit(name) {
            this.$refs[name].validate((valid) => {
                if (valid) {
                    document.getElementById("sso").submit()
                } else {
                    this.$Message.error('登录失败!');
                }
            })
        }
    }
});