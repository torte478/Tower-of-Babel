Ext.application({
    requires: [
        'Ext.container.Viewport',
        'ToB.registry.Panel'
    ],
    
    name: 'ToB',

    appFolder: 'app',

    launch: function() {
        Ext.create('Ext.container.Viewport', {
            items: [
                Ext.create('ToB.registry.Panel', {
                    layout: {
                        type: 'vbox',
                        align: 'stretch',
                    }
                })
            ]
        });
    }
});