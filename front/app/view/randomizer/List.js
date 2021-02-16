Ext.define('AM.view.randomizer.List', {
   extend: 'Ext.grid.Panel', 
   alias: 'widget.registrylist',
   title: 'Registries',
   store: 'Registries',
   
   initComponent: function() {
       this.columns = [
           { header: 'Parent', dataIndex: 'parent'},
           { header: 'Label', dataIndex: 'label'}
       ];
       
       this.callParent(arguments);
   } 
});