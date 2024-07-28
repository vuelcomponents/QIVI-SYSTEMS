export default {
  root: {
    class: [
      'relative ring-1 ring-surface-950/5 ',

      // Flexbox
      'flex',
      'items-center',
      'gap-6',

      // Spacing
      'px-6 py-4',
      'min-h-[3rem]',

      // Shape
      'rounded-md',
      'shadow-none',

      // Color
      'bg-west-2  dark:bg-surface-950',
      'ring-0 ',
    ],
  },
  menu: ({ props }) => ({
    class: [
      // Flexbox
      'sm:flex sm:row-gap-2 sm:col-gap-4',
      'items-center',
      'flex-wrap',
      'flex-col sm:flex-row',
      { hidden: !props?.mobileActive, flex: props?.mobileActive },

      // Position
      'absolute sm:relative',
      'top-full left-0',
      'sm:top-auto sm:left-auto',

      // Size
      'w-full sm:w-auto',

      // Spacing
      'm-0 ',
      'py-2 px-1.5 sm:pt-0 sm:p-0 sm:py-1.5',
      'list-none',

      // Shape
      'shadow-md sm:shadow-none',
      'border-0',
      'sm:rounded-none rounded-md',

      // Color
      'bg-west-1 dark:bg-surface-900 sm:bg-transparent dark:sm:bg-transparent',

      // Misc
      'outline-none ',
    ],
  }),
  menuitem: {
    class: 'sm:relative sm:w-auto w-full static ',
  },
  content: ({ props, context }) => ({
    class: [
      // Shape
      { 'rounded-md': props.root },

      //  Colors
      {
        'text-west-3 dark:text-surface-200': !context.focused && !context.active,
        'text-west-3 dark:text-surface-200 bg-white-1 dark:bg-black/70':
          context.focused && !context.active,
        'text-west-3 dark:text-surface-0/80 bg-white-1 dark:bg-black/70':
          context.focused && context.active,
        'text-west-3 dark:text-surface-0/80 bg-white-1  dark:bg-black/70':
          !context.focused && context.active,
      },

      // Hover States
      {
        'hover:bg-black/10 dark:hover:bg-surface-800': !context.active,
        'hover:bg-black/10 dark:hover:bg-black/40 text-surface-900 dark:text-surface-0/80':
        context.active,
      },

      // Transitions
      'transition-all',
      'duration-200 dark:text-surface-100',
    ],
  }),
  action: ({ context }) => ({
    class: [
      'relative',

      // Font
      'sm:text-sm font-medium font-mulish',

      // Flexbox
      'flex',
      'items-center',

      // Spacing
      'py-2',
      'px-3',
      'my-1 sm:my-0',

      // Size
      {
        'pl-5 sm:pl-3': context.level === 1,
        'pl-7 sm:pl-3': context.level === 2,
      },

      // Misc
      'select-none',
      'cursor-pointer',
      'no-underline ',
      'overflow-hidden',
    ],
  }),
  icon: {
    class: 'mr-2',
  },
  submenuicon: ({ props }) => ({
    class: [
      {
        'ml-auto sm:ml-2': props.root,
        'ml-auto': !props.root,
      },
    ],
  }),
  submenu: ({ props }) => ({
    class: [
      // Size
      'w-full sm:w-48',

      // Spacing
      'py-1',
      'm-0 ',
      'list-none',

      // Shape
      'shadow-none sm:shadow-md',
      'border-0',

      // Position
      'static sm:absolute',
      'z-10',
      { 'sm:absolute sm:left-full sm:top-0': props.level > 1 },

      // Color
      'bg-west-1 dark:bg-surface-900',
    ],
  }),
  separator: {
    class: 'border-t border-surface-200 dark:border-surface-600 my-1',
  },
  button: {
    class: [
      // Flexbox
      'flex sm:hidden',
      'items-center justify-center',

      // Size
      'w-8',
      'h-8',

      // Shape
      'rounded-full',
      // Color
      'text-surface-500 dark:text-white/80 ',

      // States
      'hover:text-surface-600 dark:hover:text-white/60',
      'hover:bg-surface-50 dark:hover:bg-surface-1/10',
      'focus:outline-none focus:outline-offset-0',
      'focus:ring-2 focus:ring-inset focus:ring-primary-500 dark:focus:ring-primary-400',

      // Transitions
      'transition duration-200 ease-in-out',

      // Misc
      'cursor-pointer',
      'no-underline',
    ],
  },
  end: {
    class: 'ml-auto self-center',
  },
};
