import {nodeResolve} from '@rollup/plugin-node-resolve';
import typescript from 'rollup-plugin-typescript2';

// rollup.config.js
/**
 * @type {import('rollup').RollupOptions}
 */
const config = {
    input: 'src/Main.ts',
    output: {
        dir: "dist",
        name: "main",
        format: 'esm',
        entryFileNames: "bundle.js"
    },
    plugins: [
        nodeResolve({
            preferBuiltins: false
        }),
        typescript()
    ]
};

export default config;