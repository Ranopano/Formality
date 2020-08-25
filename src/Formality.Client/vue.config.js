/* eslint-disable */
const path = require('path');

module.exports = {
  chainWebpack: (config) => {
    config
      .plugin('html')
      .tap((args) => {
        args[0].title = 'Formality';
        return args;
      });
    config.resolve.alias.set('app', path.resolve('src/app'));
  },
  css: {
    loaderOptions: {
      sass: {
        prependData: `
          @import "@/styles/_variables.scss";
        `
      }
    }
  }
};
