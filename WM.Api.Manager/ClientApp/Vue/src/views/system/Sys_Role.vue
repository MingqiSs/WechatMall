<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/system/Sys_Role.js此处编写
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
    import extend from "@/extension/system/Sys_Role.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'role_Id',
                    footer: "Foots",
                    cnName: '角色管理',
                    name: 'Sys_Role',
                    url: "/Sys_Role/",
                    sortName: "role_Id"
                },
                extend: extend,
                editFormFileds: {"parentId":"","roleName":"","deptName":"","enable":"","creator":"","createDate":"","modifier":"","modifyDate":""},
                editFormOptions: [[{"title":"父级ID","required":true,"field":"parentId","type":"number"},
                               {"title":"角色名称","field":"roleName"}],
                              [{"title":"部门名称","field":"deptName"},
                               {"dataKey":"enable","title":"是否启用","field":"enable","type":"switch"}],
                              [{"title":"创建人","field":"creator","disabled":true},
                               {"title":"创建时间","field":"createDate","disabled":true,"type":"datetime"}],
                              [{"title":"修改人","field":"modifier","disabled":true},
                               {"title":"修改时间","field":"modifyDate","disabled":true}]],
                searchFormFileds: {"roleName":"","deptName":"","enable":"","createDate":"","modifyDate":""},
                searchFormOptions: [[{"title":"角色名称","field":"roleName","type":"text"},{"title":"部门名称","field":"deptName","type":"text"},{"dataKey":"enable","title":"是否启用","field":"enable","type":"select"}],[{"title":"创建时间","field":"createDate","type":"datetime"},{"title":"修改时间","field":"modifyDate","type":"datetime"}]],
                columns: [{field:'role_Id',title:'角色ID',type:'int',width:70,readonly:true,require:true,align:'left',sortable:true},
                       {field:'parentId',title:'父级ID',type:'int',width:70,require:true,align:'left'},
                       {field:'roleName',title:'角色名称',type:'string',link:true,width:90,align:'left'},
                       {field:'dept_Id',title:'部门ID',type:'int',width:90,hidden:true,align:'left'},
                       {field:'deptName',title:'部门名称',type:'string',width:90,align:'left'},
                       {field:'enable',title:'是否启用',type:'sbyte',bind:{ key:'enable',data:[]},width:90,align:'left'},
                       {field:'sort',title:'排序',type:'int',width:90,hidden:true,align:'left'},
                       {field:'creator',title:'创建人',type:'string',width:130,readonly:true,align:'left'},
                       {field:'createDate',title:'创建时间',type:'datetime',width:90,readonly:true,align:'left',sortable:true},
                       {field:'modifier',title:'修改人',type:'string',width:130,readonly:true,align:'left'},
                       {field:'modifyDate',title:'修改时间',type:'datetime',width:90,readonly:true,align:'left',sortable:true}],
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
