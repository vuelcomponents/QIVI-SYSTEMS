import { ref } from 'vue';

export const useStepper = (step: number,
                           stepperItems:Array<{label:string,icon:string, id:number }>) => {
  const currentStep = ref<number>(step);
  const onStepChange = (id: number) => {
    currentStep.value = id;
  };

  return {
    currentStep,
    onStepChange,
    stepperItems
  };
};