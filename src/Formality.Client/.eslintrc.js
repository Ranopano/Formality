module.exports = {
  root: true,
  env: {
    node: true
  },
  extends: ["plugin:vue/essential", "@vue/airbnb", "@vue/typescript/recommended"],
  parserOptions: {
    ecmaVersion: 2020,
    ecmaFeatures: {
      jsx: true
    }
  },
  rules: {
    quotes: ["error", "single"],
    "max-len": ["error", { "code": 100, "tabWidth": 4, "ignoreComments": true }],
    "arrow-parens": ["error", "as-needed"],
    "no-plusplus": "off",
    "import/prefer-default-export": "off",
    "no-useless-constructor": "off",
    "class-methods-use-this": "off",
    "consistent-return": "off",
    "no-restricted-syntax": "off"
  },
  overrides: [
    {
      files: ["**/__tests__/*.{j,t}s?(x)", "**/tests/unit/**/*.spec.{j,t}s?(x)"],
      env: {
        jest: true
      }
    }
  ]
};
