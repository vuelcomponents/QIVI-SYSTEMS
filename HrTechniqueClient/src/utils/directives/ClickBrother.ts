export default {
  mounted: function (el: any) {
    el.addEventListener('click', (e: Event) => {
      e.stopPropagation();
      const parent = (e.target as HTMLElement)!.parentElement!.parentElement!.parentElement!;
      const buttons = parent.querySelectorAll('button');
      if (buttons.length > 1) {
        buttons[1].click();
      }
    });
  },
};
