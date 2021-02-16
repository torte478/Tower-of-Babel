Ext.application({
    requires: ['Ext.container.Viewport'],
    name: 'AM',

    appFolder: 'app',

    controllers: [
        'Registries'
    ],

    launch: function() {
        Ext.create('Ext.container.Viewport', {
            layout: 'border',
            items: {
                xtype: 'registrylist',
                region: 'center',
                margins: '5, 5, 5, 5'
            }
        });
    }
});