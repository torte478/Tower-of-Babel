Ext.application({
    requires: [
        'Ext.container.Viewport',
        'Ext.tab.Panel',
        'ToB.registry.Panel'
    ],
    
    name: 'ToB',

    appFolder: 'app',

    launch: function() {
        Ext.create('Ext.container.Viewport', {
            items: [
                Ext.create('Ext.tab.Panel', {
                    items: [
                        Ext.create('ToB.registry.Panel')
                    ]
                })
            ]
        });
    }
});