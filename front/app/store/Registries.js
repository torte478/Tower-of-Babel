Ext.define('AM.store.Registries', {
    extend: 'Ext.data.Store',
    model: 'AM.model.Registry',
    autoLoad: true, //TODO
    autoSync: true, //TODO
    
    proxy: {
        type: 'rest',
        noCache: false,
        api: {
            read: 'http://localhost:5010/api/randomizer'
        },
        reader: {
            type: 'json',
            root: 'data',
            successProperty: 'success'
        }
    }
});