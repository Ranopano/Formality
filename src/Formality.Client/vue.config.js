/* eslint-disable */
const path = require('path');

module.exports = {
  chainWebpack: config => {
    config
      .plugin('html')
      .tap(args => {
        args[0].title = 'Formality';
        return args;
      });
    config.optimization
      .minimizer('terser')
      .tap(args => {
        const { terserOptions } = args[0];
        terserOptions.keep_fnames = true; // do not butcher function names
        return args;
      });
    config.resolve.alias.set('app', path.resolve('src/app'));
  },
  css: {
    loaderOptions: {
      sass: {
        prependData: `
          @import "@/styles/_variables.scss";
        `,
      },
    },
  },
};
