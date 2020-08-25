import { shallowMount } from '@vue/test-utils';
import PageHeader from '@/app/common/components/headers/PageHeader.vue';

describe('PageHeader.vue', () => {
  it('renders props when passed', () => {
    const header = 'header';
    const description = 'description';
    const wrapper = shallowMount(PageHeader, {
      propsData: { header, description },
    });
    expect(wrapper.text()).toMatch(`${header} ${description}`);
  });
});
