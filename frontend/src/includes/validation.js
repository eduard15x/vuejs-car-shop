import {
    Form as VeeForm,
    Field as VeeField,
    defineRule,
    ErrorMessage,
    configure
} from 'vee-validate';
import {
    required,
    min,
    max,
    alpha_spaces as alphaSpaces,
    email,
    min_value as minValue,
    max_value as maxValue,
    confirmed,
    not_one_of as excluded
} from '@vee-validate/rules';

export default {
    install(app) {
        app.component('VeeForm', VeeForm);
        app.component('VeeField', VeeField);
        app.component('ErrorMessage', ErrorMessage);

        defineRule('required', required);
        defineRule('tos', required);
        defineRule('min', min);
        defineRule('max', max);
        defineRule('alpha_spaces', alphaSpaces);
        defineRule('email', email);
        defineRule('min_value', minValue);
        defineRule('max_value', maxValue);
        defineRule('confirmed', confirmed);
        defineRule('excluded', excluded);

        configure({
            generateMessage: (context) => {
                const messages = {
                    required: `The field ${context.field} is required.`,
                    min: `The field ${context.field} is too short.`,
                    max: `The field ${context.field} is too long.`,
                    alpha_spaces: `The field ${context.field} may only contain alphabetical characters.`,
                    email: `The field ${context.field} must be a valid email.`,
                    min_value: `The field ${context.field} is too low.`,
                    max_value: `The field ${context.field} is too high.`,
                    confirmed: `The passwords don't match.`,
                    excluded: `You are not allowed to use this value for the field ${context.field}.`,
                    tos: 'You must accept the terms of service.'
                }

                return messages[context.rule.name] ? messages[context.rule.name] : `The field ${context.field} is invalid.`;
            },
            validateOnBlur: true,
            validateOnChange: true,
            validateOnInput: false,
            validateOnModelUpdate: true
        });
    }
}