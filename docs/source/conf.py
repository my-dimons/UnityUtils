import os
# Configuration file for the Sphinx documentation builder.

# -- Project information

project = 'Unity Utils'
copyright = '2025, mydimons'
author = 'mydimons'

release = '1.0'
version = '1.3.0'

# -- General configuration

extensions = [
    'sphinx.ext.duration',
    'sphinx.ext.doctest',
    'sphinx.ext.autodoc',
    'sphinx.ext.autosummary',
    'sphinx.ext.intersphinx',
    'breathe'
]

breathe_projects = {
    "UnityUtils": os.path.join(os.path.dirname(__file__), "doxygen", "xml")
}

suppress_warnings = ["breathe.*"]

breathe_default_project = "UnityUtils"

intersphinx_mapping = {
    'python': ('https://docs.python.org/3/', None),
    'sphinx': ('https://www.sphinx-doc.org/en/master/', None),
}
intersphinx_disabled_domains = ['std']

templates_path = ['_templates']

# -- Options for HTML output

html_theme = 'sphinx_rtd_theme'

# -- Options for EPUB output
epub_show_urls = 'footnote'