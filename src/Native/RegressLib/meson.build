
regress_deps = [gobject_dep, gio_dep]
regress_c_args = ['-D_GI_DISABLE_CAIRO']
custom_c_args = []

utility_sources = ['utility.c']
utility_headers = ['utility.h']

utility_sources = ['utility.c', 'utility.h']
utility_deps = []
utility_lib = shared_library('utility-1.0',
  sources: utility_sources,
  include_directories : [include_directories('.')],
  c_args: custom_c_args,
  dependencies: [gobject_dep],
  install: true
)

regress_sources = ['regress.c', 'annotation.c', 'foo.c', 'drawable.c', 'misc.c']
regress_headers = ['regress.h', 'annotation.h', 'foo.h', 'drawable.h', 'misc.h']

regress_lib = shared_library('regress-1.0',
  sources: regress_sources,
  include_directories : [include_directories('.')],
  link_with: utility_lib,
  c_args: regress_c_args + custom_c_args,
  dependencies: regress_deps,
  install: true
)

gnome = import('gnome')
utility_gir = gnome.generate_gir(
  utility_lib,
  sources: utility_sources + utility_headers,
  namespace: 'Utility',
  nsversion: '1.0',
  identifier_prefix: 'Utility',
  symbol_prefix: 'utility',
  export_packages: 'utility',
  dependencies: utility_deps,
  includes: ['GLib-2.0', 'GObject-2.0'],
  header: 'utility.h',
  install: true,
  extra_args: custom_c_args,
)

regress_gir = gnome.generate_gir(
  regress_lib,
  sources: regress_sources + regress_headers,
  namespace: 'Regress',
  nsversion: '1.0',
  identifier_prefix: 'Regress',
  symbol_prefix: 'regress',
  export_packages: 'regress',
  dependencies: regress_deps,
  includes: ['GLib-2.0', 'GObject-2.0', 'Gio-2.0', utility_gir[0]],
  header: 'regress.h',
  install: true,
  extra_args: custom_c_args + regress_c_args,
)