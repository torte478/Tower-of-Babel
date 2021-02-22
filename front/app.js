Ext.application({
    requires: [
        'Ext.container.Viewport',
        'ToB.Registry.Panel'
    ],
    
    name: 'ToB',

    appFolder: 'app',

    launch: function() {
        Ext.create('Ext.container.Viewport', {
            items: [
                Ext.create('ToB.Registry.Panel', {
                    layout: {
                        type: 'vbox',
                        align: 'stretch',
                    }
                })
            ]
        });
    }
});