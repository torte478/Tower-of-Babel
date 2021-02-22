Ext.define('ToB.Registry.Grid', {
    extend: 'Ext.grid.Panel',
    
    requires: ['ToB.Registry.Store'],
    
    store: Ext.create('ToB.Registry.Store'),
    
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