Ext.define('ToB.registry.Grid', {
    extend: 'Ext.grid.Panel',
    
    requires: ['ToB.registry.Store'],
    
    store: Ext.create('ToB.registry.Store'),
    
    columns: [
        {
            header: 'Id',
            dataIndex: 'id',
            width: 50
        },
        {
            header: 'Label',
            dataIndex: 'label',
            flex: 1
        }
    ],
})