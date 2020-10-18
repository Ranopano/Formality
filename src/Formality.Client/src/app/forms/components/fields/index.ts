import { Prop, Vue } from 'vue-property-decorator';
import {
  FormFieldDto,
  FormFieldRuleDto,
  FieldRule,
  LengthData,
} from '../../models';

export type FormFieldModel = {
  value: string;
  onUpdate: (value: string) => void;
};

export abstract class FieldBase extends Vue {
  @Prop({ required: true })
  public field!: FormFieldDto;

  @Prop({ required: false })
  public model?: FormFieldModel;

  private fieldValue = '';

  protected get value() {
    return this.fieldValue;
  }

  protected set value(value: string | undefined) {
    this.fieldValue = value || '';

    if (this.model) {
      this.model.onUpdate(value || '');
    }
  }

  protected get rules() {
    return this.field.rules.map(this.matchRule).join('|');
  }

  // TODO: clean up
  private matchRule = (rule: FormFieldRuleDto): string | undefined => {
    switch (rule.type) {
      case FieldRule.Required:
        return 'required';
      case FieldRule.Length: {
        const data = JSON.parse(rule.data || '{}') as LengthData;
        const min = data.minLength !== undefined ? `min:${data.minLength}` : '';
        const max = data.maxLength !== undefined ? `max:${data.maxLength}` : '';
        return [min, max].filter(x => x).join('|');
      }
      default:
        return undefined;
    }
  };
}
