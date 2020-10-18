module.exports = {
  root: true,
  env: {
    node: true
  },
  extends: [
    "plugin:vue/essential",
    "eslint:recommended",
    "@vue/typescript/recommended",
    "@vue/prettier",
    "@vue/prettier/@typescript-eslint"
  ],
  parserOptions: {
    ecmaVersion: 2020,
    ecmaFeatures: {
      jsx: true
    }
  },
  rules: {
    "arrow-parens": ["error", "as-needed"],
    "arrow-body-style": "off",
    "no-plusplus": "off",
    "import/prefer-default-export": "off",
    "no-useless-constructor": "off",
    "class-methods-use-this": "off",
    "consistent-return": "off",
    "no-restricted-syntax": "off",
    "no-param-reassign": ["error", { props: false }],
    "prettier/prettier": [
      "warn",
      {
        "arrowParens": "avoid",
        "singleQuote": true,
        "endOfLine": "lf",
        "trailingComma": "es5",
        "printWidth": 80,
      }
    ],
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
