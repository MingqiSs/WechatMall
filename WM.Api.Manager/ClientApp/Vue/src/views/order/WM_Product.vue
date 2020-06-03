<!--
*Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *业务请在@/extension/order/WM_Product.js此处编写
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
    import extend from "@/extension/order/WM_Product.js";
    import ViewGrid from "@/components/basic/ViewGrid.vue";
    var vueParam = {
        components: {
            ViewGrid
        },
        data() {
            return {
                table: {
                    key: 'ID',
                    footer: "Foots",
                    cnName: '商品',
                    name: 'WM_Product',
                    url: "/WM_Product/",
                    sortName: "Sort"
                },
                extend: extend,
                editFormFileds: {"Name":"","ProductTypeID":"","Describe":""},
                editFormOptions: [[{"title":"商品名称","required":true,"field":"Name","type":"text"}],
                              [{"dataKey":"ordertype","title":"商品分类id","required":true,"field":"ProductTypeID","type":"select"},
                               {"title":"商品描述","required":true,"field":"Describe"}]],
                searchFormFileds: {"Name":""},
                searchFormOptions: [[{"title":"商品名称","field":"Name","type":"text"}]],
                columns: [{field:'Name',title:'商品名称',type:'string',width:120,require:true,align:'left',sortable:true},
                       {field:'Icon',title:'图标',type:'img',width:120,align:'left'},
                       {field:'Inventory',title:'库存',type:'int',width:120,require:true,align:'left'},
                       {field:'Sales',title:'销量',type:'int',width:120,require:true,align:'left'},
                       {field:'Price',title:'价格',type:'decimal',width:120,require:true,align:'left'},
                       {field:'ProductTypeID',title:'商品分类id',type:'int',bind:{ key:'ordertype',data:[]},width:120,require:true,align:'left'},
                       {field:'Describe',title:'商品描述',type:'string',width:120,require:true,align:'left'},
                       {field:'Image',title:'图片',type:'img',width:120,align:'left'},
                       {field:'CreateDate',title:'创建时间',type:'datetime',width:120,align:'left',sortable:true},
                       {field:'Remark',title:'备注',type:'string',width:120,align:'left'},
                       {field:'ModifyID',title:'ModifyID',type:'int',width:120,hidden:true,align:'left'},
                       {field:'ModifyDate',title:'修改日期',type:'datetime',width:120,align:'left',sortable:true},
                       {field:'Modifier',title:'修改人',type:'string',width:120,align:'left'},
                       {field:'ID',title:'商品ID',type:'string',width:120,hidden:true,readonly:true,require:true,align:'left'},
                       {field:'Hot',title:'是否置顶',type:'sbyte',width:120,require:true,align:'left'},
                       {field:'DataStatus',title:'数据状态',type:'sbyte',width:120,require:true,align:'left'},
                       {field:'Creator',title:'创建人',type:'string',width:120,align:'left'},
                       {field:'CreateID',title:'CreateID',type:'int',width:120,hidden:true,align:'left'},
                       {field:'Sort',title:'排序号',type:'int',width:120,hidden:true,require:true,align:'left'}],
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
