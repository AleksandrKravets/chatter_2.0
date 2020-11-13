const CracoLessPlugin = require('craco-less');

module.exports = {
  plugins: [
    {
      plugin: CracoLessPlugin,
      options: {
        lessLoaderOptions: {
          lessOptions: {
            modifyVars: { 
                '@layout-header-background': '#333333',
                '@layout-header-height': 'auto',
                '@layout-header-padding': '0',
                '@layout-header-color': '#e2e2e2',
                '@layout-footer-padding': '0',
                '@layout-footer-background': '#333333',
                '@btn-primary-bg': '#1a1a1a',
                '@btn-circle-size-lg': '60px',
                '@btn-circle-size': '60px',
                '@height-lg': '60px',
                '@btn-font-weight': '600',
                '@btn-font-size-lg': '24px',
                '@primary-color': '#bb86fc',
                '@layout-body-background': '#1a1a1a',

                // '@component-background': '#333333',
                // '@text-color': '#e2e2e2',
                // '@input-bg': 'white',
                // '@input-border-color': '#1a1a1a',

                // '@border-color-base': 'red'
                // '@border-color-inverse': 'red'
                // '@border-width-base': '1px'
            },
            javascriptEnabled: true,
          },
        },
      },
    },
  ],
};