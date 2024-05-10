const colors = require('tailwindcss/colors')
module.exports = {
    content: [
        '**/*.html',
        '**/*.razor',
        '**/*.razor.cs',
        './src/Modules/Projects/Modules.Projects.Presentation/**/*.razor',
        './src/Modules/**/*.razor.cs',
        './src/Web/**/*.razor',
        './src/InnovationPlatform.Web/**/*.razor.cs',
        './node_modules/flowbite/**/*.js',
    ],
    theme: {
        extend: {},
    },
    plugins: [
        require('flowbite/plugin')
    ],
    safelist: [
        'text-gray-600',
        'text-red-600',
        'text-yellow-600',
        'text-blue-600',
        'bg-blue-600',
        'bg-gray-100',
        'bg-gray-400',
        'bg-gray-600',
        'bg-green-600',
        'bg-yellow-600',
        'bg-red-600',
        'px-6',
        'py-3'
    ],
}
