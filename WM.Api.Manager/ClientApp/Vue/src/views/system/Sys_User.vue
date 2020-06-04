<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/system/Sys_User.js此处编写
 -->
<template>
    <div>
        <view-grid :columns="columns"
                   :detail="detail"
                   :editFormFileds="editFormFileds"
                   :editFormOptions="editFormOptions"
                   :searchFormFileds="searchFormFileds"
                   :searchFormOptions="searchFormOptions"
                   :table="table"
                   :extend="extend">
        </view-grid>
    </div>
</template>

<script>
    import extend from "@/extension/system/Sys_User.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'User_Id',
                    footer: "Foots",
                    cnName: '用户管理',
                    name: 'Sys_User',
                    url: "/Sys_User/",
                    sortName: "User_Id"
                },
                extend: extend,
                editFormFileds: {"userName":"","userTrueName":"","Role_Id":"","gender":"","enable":"","createDate":"","creator":"","remark":"","headImageUrl":""},
                editFormOptions: [[{"title":"帐号","required":true,"field":"userName","disabled":true},
                               {"title":"真实姓名","required":true,"field":"userTrueName","type":"text"}],
                              [{"dataKey":"roles","title":"角色","required":true,"field":"Role_Id","type":"drop"}],
                              [{"dataKey":"gender","title":"性别","field":"Gender","type":"drop"},
                               {"dataKey":"enable","title":"是否可用","required":true,"field":"Enable","type":"drop"}],
                              [{"title":"注册时间","field":"createDate","disabled":true},
                               {"title":"创建人","field":"creator","disabled":true}],
                              [{"title":"备注","field":"remark","colSize":12,"type":"textarea"}],
                              [{"title":"头像","field":"headImageUrl","type":"img"}]],
                searchFormFileds: {"userName":"","gender":""},
                searchFormOptions: [[{"title":"帐号","field":"userName"},{"dataKey":"gender","title":"性别","field":"Gender","type":"drop"},{"title":"真实姓名","field":"UserTrueName"}],[{"title":"部门","field":"DeptName"},{"dataKey":"roles","title":"角色","field":"Role_Id","type":"drop"},{"title":"Token","field":"Token"}],[{"dataKey":"ut","title":"登陆设备类型","field":"AppType","type":"dropList"},{"dataKey":"isphone","title":"手机用户","field":"IsRegregisterPhone","type":"drop"},{"title":"手机号","field":"PhoneNo"}],[{"dataKey":"enable","title":"是否可用","field":"Enable","type":"drop"},{"title":"地址","field":"Address"},{"title":"Email","field":"Email"}],[{"title":"注册时间","field":"CreateDate","type":"datetime"},{"title":"最后登陆时间","field":"LastLoginDate","type":"datetime"}]],
                columns: [{field:'userName',title:'帐号',type:'string',link:true,width:120,readonly:true,require:true,align:'left',sortable:true},
                       {field:'uid',title:'uid',type:'int',width:90,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'gender',title:'性别',type:'int',bind:{ key:'gender',data:[]},width:100,align:'left'},
                       {field:'headImageUrl',title:'头像',type:'img',width:150,align:'left'},
                       {field:'dept_Id',title:'Dept_Id',type:'int',width:90,hidden:true,align:'left'},
                       {field:'deptName',title:'部门',type:'string',width:150,hidden:true,align:'left'},
                       {field:'role_Id',title:'角色',type:'int',bind:{ key:'roles',data:[]},width:150,require:true,align:'left'},
                       {field:'roleName',title:'roleName',type:'string',width:90,hidden:true,require:true,align:'left'},
                       {field:'token',title:'Token',type:'string',width:180,hidden:true,align:'left'},
                       {field:'appType',title:'登陆设备类型',type:'int',bind:{ key:'ut',data:[]},width:150,hidden:true,align:'left'},
                       {field:'userTrueName',title:'真实姓名',type:'string',width:150,require:true,align:'left'},
                       {field:'createDate',title:'注册时间',type:'datetime',width:150,readonly:true,align:'left',sortable:true},
                       {field:'IsRegregisterPhone',title:'手机用户',type:'int',bind:{ key:'isphone',data:[]},width:120,hidden:true,require:true,align:'left'},
                       {field:'phoneNo',title:'手机号',type:'string',width:150,hidden:true,align:'left'},
                       {field:'tel',title:'Tel',type:'string',width:90,hidden:true,align:'left'},
                       {field:'createID',title:'CreateID',type:'int',width:90,hidden:true,align:'left'},
                       {field:'creator',title:'创建人',type:'string',width:130,readonly:true,align:'left'},
                       {field:'enable',title:'是否可用',type:'byte',bind:{ key:'enable',data:[]},width:90,require:true,align:'left'},
                       {field:'modifyID',title:'ModifyID',type:'int',width:90,hidden:true,align:'left'},
                       {field:'modifier',title:'修改人',type:'string',width:130,hidden:true,readonly:true,align:'left'},
                       {field:'modifyDate',title:'修改时间',type:'datetime',width:90,readonly:true,align:'left',sortable:true},
                       {field:'auditStatus',title:'审核状态',type:'int',bind:{ key:'audit',data:[]},width:90,hidden:true,align:'left'},
                       {field:'auditor',title:'审核人',type:'string',width:90,hidden:true,align:'left'},
                       {field:'auditDate',title:'审核时间',type:'datetime',width:150,hidden:true,align:'left',sortable:true},
                       {field:'lastLoginDate',title:'最后登陆时间',type:'datetime',width:150,hidden:true,align:'left',sortable:true},
                       {field:'lastModifyPwdDate',title:'最后密码修改时间',type:'datetime',width:150,hidden:true,align:'left',sortable:true},
                       {field:'address',title:'地址',type:'string',width:190,hidden:true,align:'left'},
                       {field:'mobile',title:'电话',type:'string',width:140,hidden:true,align:'left'},
                       {field:'email',title:'Email',type:'string',width:140,hidden:true,align:'left'},
                       {field:'remark',title:'备注',type:'string',width:180,hidden:true,align:'left'},
                       {field:'sort',title:'排序号',type:'int',width:90,hidden:true,align:'left'}],
                detail: {
                    cnName:"#detailCnName",
                    columns: [],
                    sortName: "",
                    key:""
                }
            };
        }
    };
    export default vueParam;
</script>
