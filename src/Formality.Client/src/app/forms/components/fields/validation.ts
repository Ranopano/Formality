import { extend } from 'vee-validate';
import * as rules from 'vee-validate/dist/rules';

// eslint-disable-next-line @typescript-eslint/no-var-requires
const { messages } = require('vee-validate/dist/locale/en.json');

Object.keys(rules).forEach(rule => {
  extend(rule, {
    ...rules[rule],
    message: messages[rule],
  });
});
